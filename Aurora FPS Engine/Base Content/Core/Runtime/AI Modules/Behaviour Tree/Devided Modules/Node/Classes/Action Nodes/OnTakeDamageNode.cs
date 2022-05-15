using AuroraFPSRuntime.AIModules.BehaviourTree.Attributes;
using AuroraFPSRuntime.AIModules.BehaviourTree.Variables;
using AuroraFPSRuntime.SystemModules.HealthModules;
using UnityEngine;

namespace AuroraFPSRuntime.AIModules.BehaviourTree.Nodes
{
    [TreeNodeContent("On Take Damage", "Conditions/On Take Damage")]
    public class OnTakeDamageNode : ActionNode
    {
        [SerializeField]
        [TreeVariable(typeof(bool))]
        private string storedVariable;

        // Stored required components.
        private ObjectHealth objectHealth;

        // Stored required properties.
        private bool takenDamage;

        protected override void OnInitialize()
        {
            objectHealth = owner.GetComponent<ObjectHealth>();
            objectHealth.OnTakeDamageCallback += OnTakeDamageCallback;
        }

        protected override State OnUpdate()
        {
            if (takenDamage)
            {
                takenDamage = false;

                if (tree.TryGetVariable<BoolVariable>(storedVariable, out BoolVariable boolVariable))
                {
                    boolVariable.SetValue(true);
                }

                return State.Success;
            }

            return State.Failure;
        }

        private void OnTakeDamageCallback(float amount)
        {
            takenDamage = true;
        }
    }
}