using Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Tests
{
	public sealed class RenderTest : MonoBehaviour
	{
		[SerializeField] private RenderQualityEnum renderQuality;
		[SerializeField] private UniversalRenderPipelineAsset target;

		private void Start()
		{
			QualitySettings.SetQualityLevel((int) renderQuality);
			QualitySettings.renderPipeline = target;
		}
	}
}