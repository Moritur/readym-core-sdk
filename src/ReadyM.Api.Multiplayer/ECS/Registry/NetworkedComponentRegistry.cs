using System;
using System.Collections.Generic;
using LiteNetLib;
using Microsoft.Extensions.Logging;
using ReadyM.Api.ECS.Registry;
using ReadyM.Api.Multiplayer.ECS.Components;
using ReadyM.Api.Multiplayer.Interning;

namespace ReadyM.Api.Multiplayer.ECS.Registry;

internal class NetworkedComponentRegistry(IEnumerable<INetworkedComponentRegistration> registrations, ILogger logger)
    : IdComponentRegistryBase<INetworkedComponentRegistry, INetworkedComponent>(registrations), INetworkedComponentRegistry
{
    protected readonly ILogger Logger = logger;
    protected readonly Dictionary<string, (NetworkedComponentId Id, DeliveryMethod DeliveryMethod)> ComponentIds = new();
    protected readonly Dictionary<NetworkedComponentId, Type> NetComponentTypes = new();

    public INetworkedComponentRegistry RegisterComponent(Type componentType, DeliveryMethod deliveryMethod = DeliveryMethod.ReliableOrdered)
    {
        if (!typeof(INetworkedComponent).IsAssignableFrom(componentType))
            throw new ArgumentException($"Type {componentType.FullName} does not implement INetworkedComponent", nameof(componentType));

        RequireDeltaSafe(deliveryMethod, componentType.FullName!);
        RequireInternedSafe(componentType, deliveryMethod);

        var id = new NetworkedComponentId(GetNextComponentId());
        ComponentIds.Add(componentType.FullName!, (id, deliveryMethod));
        NetComponentTypes.Add(id, componentType);

        Logger.LogInformation("Registered networked component {Id}: {ComponentType} ({ComponentFullName}) delivery {DeliveryMethod}", id, componentType.Name, componentType.FullName, deliveryMethod);
        return base.RegisterComponentImpl(componentType);
    }

    public INetworkedComponentRegistry RegisterComponent<T>(DeliveryMethod deliveryMethod = DeliveryMethod.ReliableOrdered)
        where T : struct, INetworkedComponent
    {
        RequireDeltaSafe(deliveryMethod, typeof(T).FullName!);
        RequireInternedSafe(typeof(T), deliveryMethod);

        var id = new NetworkedComponentId(GetNextComponentId());
        ComponentIds.Add(typeof(T).FullName!, (id, deliveryMethod));
        NetComponentTypes.Add(id, typeof(T));

        Logger.LogInformation("Registered networked component {Id}: {ComponentType} ({ComponentFullName}) delivery {DeliveryMethod}", id, typeof(T).Name, typeof(T).FullName, deliveryMethod);
        return base.RegisterComponentImpl(default(T));
    }

    public NetworkedComponentId GetNetworkedComponentId(Type type)
        => ComponentIds[type.FullName!].Id;

    public NetworkedComponentId GetNetworkedComponentId<T>()
        => ComponentIds[typeof(T).FullName!].Id;

    public NetworkedComponentId GetNetworkedComponentId(string typeFullName)
        => ComponentIds[typeFullName].Id;

    public Type GetComponentType(NetworkedComponentId componentId)
        => NetComponentTypes[componentId];

    public DeliveryMethod GetNetworkedComponentDeliveryMethod<T>()
        => ComponentIds[typeof(T).FullName!].DeliveryMethod;

    // A delta carries only the entities and fields that changed since the previous one, all of it on one channel,
    // and nothing sends it again. Sequenced drops every message but the newest and unordered can apply an older
    // value over a newer one, either of which leaves the state wrong until that field changes again
    protected static void RequireDeltaSafe(DeliveryMethod deliveryMethod, string component)
    {
        if (deliveryMethod is DeliveryMethod.Unreliable or DeliveryMethod.ReliableOrdered)
            return;

        throw new ArgumentException(
            $"{component} is registered with {deliveryMethod}. A networked component takes Unreliable, for a value that is " +
            "sent again whenever it changes, or ReliableOrdered for anything else.",
            nameof(deliveryMethod));
    }

    // An id is only readable after the reliable-ordered message carrying its string, and an unreliable delta can
    // overtake that message
    protected static void RequireInternedSafe(Type componentType, DeliveryMethod deliveryMethod)
    {
        if (deliveryMethod == DeliveryMethod.ReliableOrdered || !typeof(IInternedComponent).IsAssignableFrom(componentType))
            return;

        throw new ArgumentException(
            $"{componentType.FullName} is registered with {deliveryMethod}, but has a field marked [Interned], which " +
            "needs ReliableOrdered.",
            nameof(deliveryMethod));
    }
}