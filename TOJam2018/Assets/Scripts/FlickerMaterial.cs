using System.Collections;
using UnityEngine;

namespace TOJAM2018.Gameplay
{
    public class FlickerMaterial : MonoBehaviour
    {
        public MeshRenderer meshRenderer;

        public void StartFlicker()
        {
            /*for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                meshRenderer.materials[i].color = Color.red;
            }*/
            StartCoroutine(FlickerRed());
        }

        IEnumerator FlickerRed()
        {
            Color[] originalColors = new Color[meshRenderer.materials.Length];
            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                originalColors[i] = meshRenderer.materials[i].color;
            }

            while (true)
            {
                for (int i = 0; i < meshRenderer.materials.Length; i++)
                {
                    meshRenderer.materials[i].color = Color.red;
                }
                yield return new WaitForSeconds(0.4f);
                for (int i = 0; i < meshRenderer.materials.Length; i++)
                {
                    meshRenderer.materials[i].color = originalColors[i];
                }
                yield return new WaitForSeconds(0.2f);
                yield return 0;
            }
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}