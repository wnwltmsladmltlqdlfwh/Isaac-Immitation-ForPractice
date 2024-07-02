//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Script/Player/PlayerInputSystem.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputSystem: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputSystem()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputSystem"",
    ""maps"": [
        {
            ""name"": ""CharactorActionsMap"",
            ""id"": ""1415321e-8edf-464f-92ee-76cb8e70479c"",
            ""actions"": [
                {
                    ""name"": ""MoveChar"",
                    ""type"": ""Value"",
                    ""id"": ""391442b8-e7f8-4f5e-ba31-daea421f1782"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""NormalizeVector2"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""BulletDir"",
                    ""type"": ""Value"",
                    ""id"": ""81c15a3a-9d4a-49f8-8356-d2244fe880a9"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""063de134-1524-46e9-97df-c1a73367e641"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveChar"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c937a151-122f-4642-9afc-7cd83ac83eb8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""MoveChar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""3c197eec-d9a9-4d5f-99bb-c4f737f0b046"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""MoveChar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1a54d689-2b40-477b-aa2c-7e672f694c53"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""MoveChar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fade3d21-4696-4154-a863-265d447284b6"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoardAndMouse"",
                    ""action"": ""MoveChar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow"",
                    ""id"": ""101b11ed-49e3-4916-97b1-5d5a952d5e43"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BulletDir"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""8c329a52-08a6-4621-aac9-f3e9572c4790"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BulletDir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""bdff547a-a9ee-4b71-aecb-f1cd66bcf901"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BulletDir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""da1495be-9da9-4908-b53f-5fea7d2c922c"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BulletDir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a6d659a4-a0fd-4b2b-922b-289b55d173e1"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""BulletDir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyBoardAndMouse"",
            ""bindingGroup"": ""KeyBoardAndMouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // CharactorActionsMap
        m_CharactorActionsMap = asset.FindActionMap("CharactorActionsMap", throwIfNotFound: true);
        m_CharactorActionsMap_MoveChar = m_CharactorActionsMap.FindAction("MoveChar", throwIfNotFound: true);
        m_CharactorActionsMap_BulletDir = m_CharactorActionsMap.FindAction("BulletDir", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // CharactorActionsMap
    private readonly InputActionMap m_CharactorActionsMap;
    private List<ICharactorActionsMapActions> m_CharactorActionsMapActionsCallbackInterfaces = new List<ICharactorActionsMapActions>();
    private readonly InputAction m_CharactorActionsMap_MoveChar;
    private readonly InputAction m_CharactorActionsMap_BulletDir;
    public struct CharactorActionsMapActions
    {
        private @PlayerInputSystem m_Wrapper;
        public CharactorActionsMapActions(@PlayerInputSystem wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveChar => m_Wrapper.m_CharactorActionsMap_MoveChar;
        public InputAction @BulletDir => m_Wrapper.m_CharactorActionsMap_BulletDir;
        public InputActionMap Get() { return m_Wrapper.m_CharactorActionsMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharactorActionsMapActions set) { return set.Get(); }
        public void AddCallbacks(ICharactorActionsMapActions instance)
        {
            if (instance == null || m_Wrapper.m_CharactorActionsMapActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CharactorActionsMapActionsCallbackInterfaces.Add(instance);
            @MoveChar.started += instance.OnMoveChar;
            @MoveChar.performed += instance.OnMoveChar;
            @MoveChar.canceled += instance.OnMoveChar;
            @BulletDir.started += instance.OnBulletDir;
            @BulletDir.performed += instance.OnBulletDir;
            @BulletDir.canceled += instance.OnBulletDir;
        }

        private void UnregisterCallbacks(ICharactorActionsMapActions instance)
        {
            @MoveChar.started -= instance.OnMoveChar;
            @MoveChar.performed -= instance.OnMoveChar;
            @MoveChar.canceled -= instance.OnMoveChar;
            @BulletDir.started -= instance.OnBulletDir;
            @BulletDir.performed -= instance.OnBulletDir;
            @BulletDir.canceled -= instance.OnBulletDir;
        }

        public void RemoveCallbacks(ICharactorActionsMapActions instance)
        {
            if (m_Wrapper.m_CharactorActionsMapActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICharactorActionsMapActions instance)
        {
            foreach (var item in m_Wrapper.m_CharactorActionsMapActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CharactorActionsMapActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CharactorActionsMapActions @CharactorActionsMap => new CharactorActionsMapActions(this);
    private int m_KeyBoardAndMouseSchemeIndex = -1;
    public InputControlScheme KeyBoardAndMouseScheme
    {
        get
        {
            if (m_KeyBoardAndMouseSchemeIndex == -1) m_KeyBoardAndMouseSchemeIndex = asset.FindControlSchemeIndex("KeyBoardAndMouse");
            return asset.controlSchemes[m_KeyBoardAndMouseSchemeIndex];
        }
    }
    public interface ICharactorActionsMapActions
    {
        void OnMoveChar(InputAction.CallbackContext context);
        void OnBulletDir(InputAction.CallbackContext context);
    }
}
