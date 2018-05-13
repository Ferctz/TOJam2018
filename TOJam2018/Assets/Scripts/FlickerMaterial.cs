using System.Collections;
using UnityEngine;

namespace TOJAM2018.Gameplay
{
    /// <summary>
    /// Class that can flicker all materials on a renderer red, and back.
    /// </summary>
    public class FlickerMaterial : MonoBehaviour
    {
        public MeshRenderer meshRenderer;

        /// <summary>
        /// Called externally to start flickering this renderer's materials.
        /// </summary>
        public void StartFlicker()
        {
            StartCoroutine(FlickerRed());
        }

        /// <summary>
        /// IEnumerator that flickers all materials on this renderer red, and back
        /// in varying intervals
        /// </summary>
        /// <returns></returns>
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