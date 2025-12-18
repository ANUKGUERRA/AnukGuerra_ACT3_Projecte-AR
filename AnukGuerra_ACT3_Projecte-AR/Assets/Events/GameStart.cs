using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/GameStart")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "GameStart", message: "GameStart", category: "Events", id: "92a26aa1ceed6e08ae6aa7215c6d14b2")]
public sealed partial class GameStart : EventChannel { }

