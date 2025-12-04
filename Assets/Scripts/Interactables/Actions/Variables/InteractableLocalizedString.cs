using Localization.Text;
using Logic.String;
using UnityEngine;
using Variables;

namespace Interactables.Actions.Variables
{
    [AddComponentMenu("Interactables/Variables/Set Localized String")]

    public class InteractableLocalizedString : Interactable
    
    {
            [SerializeField] private LogicStringLocalized variable;
            [SerializeField] private TextObject newTextObject;
		
            public override void Interact() => variable.SetLocalizedText(newTextObject);
            
    }
        
    }