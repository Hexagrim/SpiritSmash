using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class NaturalMotionBlurFeature : ScriptableRendererFeature
{
    class BlurPass : ScriptableRenderPass
    {
        private Material material;
        private RenderTargetIdentifier source;
        private RenderTargetHandle tempTexture;

        public BlurPass(Material mat)
        {
            material = mat;
            tempTexture.Init("_TempMotionBlurTex");
            renderPassEvent = RenderPassEvent.AfterRenderingOpaques;
        }

        public void Setup(RenderTargetIdentifier src)
        {
            source = src;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get("Natural Motion Blur");

            RenderTextureDescriptor desc = renderingData.cameraData.cameraTargetDescriptor;
            cmd.GetTemporaryRT(tempTexture.id, desc);

            Blit(cmd, source, tempTexture.Identifier(), material);
            Blit(cmd, tempTexture.Identifier(), source);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            if (cmd == null) return;
            cmd.ReleaseTemporaryRT(tempTexture.id);
        }
    }

    public Material blurMaterial;
    private BlurPass blurPass;

    public override void Create()
    {
        blurPass = new BlurPass(blurMaterial);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (blurMaterial == null) return;

        blurPass.Setup(renderer.cameraColorTarget);
        renderer.EnqueuePass(blurPass);
    }
}
