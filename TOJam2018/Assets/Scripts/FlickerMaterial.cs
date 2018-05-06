using System.Collections;
using UnityEngine;

namespace TOJAM2018.Gameplay
{
    public class FlickerMaterial : MonoBehaviour
    {
        public MeshRenderer meshRenderer;
        private Material flickerMaterial;

        private void Awake()
        {
            flickerMaterial = meshRenderer.material;
        }

        public void StartFlicker()
        {
            for (int i = 0; i < meshRenderer.materials.Length; i++)
            {
                meshRenderer.materials[i].color = Color.red;
            }
            //StartCoroutine(Flicker());
        }

        IEnumerator Flicker()
        {
            float timer = 0f;
            Color initialColor = flickerMaterial.color;
            Color endColor = new Color(1f, 1f, 1f, flickerMaterial.color.a);
            float lerpDuration = 1f;
            bool upLerp = true;
            while (true)
            {
                if (upLerp)
                {
                    flickerMaterial.color = Color.Lerp(initialColor, endColor, timer / lerpDuration);
                }
                else
                {
                    flickerMaterial.color = Color.Lerp(endColor, initialColor, timer / lerpDuration);
                }

                timer += Time.deltaTime;
                if (timer > lerpDuration)
                {
                    timer = 0f;
                    upLerp = !upLerp;
                }
                yield return new WaitForEndOfFrame();
            }
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }
    }
}