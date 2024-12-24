
using System.Reflection;
using UnityEngine;

public class ComponentInjectionProcessor : MonoBehaviour
{
    //DO NOT REFACTOR TO SINGLETON
    //CURRENTLY CALLED THE FIRST IN SCRIPT EXECUTION ORDER
    private const string LOG_SUFFIX = " script injection failed. Stop using Pete's tools if youre not going to use it correctly.";
    private void Awake()
    {
        MonoBehaviour[] scripts = UnityEngine.Object.FindObjectsOfType<MonoBehaviour>();
        foreach (MonoBehaviour monoB in scripts)
        {
            var fields = monoB.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            foreach (var field in fields)
            {
                var attributes = field.GetCustomAttributes(true);

                foreach (var attribute in attributes)
                {
                  
                    switch (attribute)
                    {
                        case GetFromChildAttribute:
                            var tryGetChild = monoB.GetComponentInChildren(field.FieldType);
                            if(tryGetChild)
                                field.SetValue(monoB, tryGetChild);
                            else
                                Debug.LogError("GET FROM CHILD ON " + monoB.GetType() + LOG_SUFFIX);
                            break;
                        
                        case GetFromSelfAttribute:
                            var tryGetSelf = monoB.GetComponent(field.FieldType);
                            if(tryGetSelf)
                                field.SetValue(monoB, tryGetSelf);
                            else
                                Debug.LogError("GET FROM SELF ON " + monoB.GetType() + LOG_SUFFIX);
                            break;
                        
                        case GetFromParentAttribute:
                            var tryGetParent = monoB.GetComponentInParent(field.FieldType);
                            if (tryGetParent)
                                field.SetValue(monoB, tryGetParent);
                            else
                                Debug.LogError("GET FROM PARENT ON " + monoB.GetType() + LOG_SUFFIX);
                                
                            break;
                        default:
                            break;
                    }
                }
            }
        }
       
    }
}
