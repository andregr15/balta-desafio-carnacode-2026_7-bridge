// DESAFIO: Sistema de Notificações Multi-Plataforma
// PROBLEMA: Um aplicativo precisa exibir notificações em diferentes plataformas (Web, Mobile, Desktop)
// com diferentes tipos de conteúdo (Texto, Imagem, Vídeo). O código atual cria uma explosão de classes
// combinando cada tipo de notificação com cada plataforma
namespace DesignPatternChallenge
{
    // Contexto: Sistema que renderiza notificações em múltiplas plataformas
    // Cada combinação de tipo + plataforma requer código específico

    // Problema: Explosão combinatória de classes
    // 3 tipos × 3 plataformas = 9 classes concretas!

    public interface IPlatform
    {
        void RenderTextNotification(string title, string content);
        void RenderImageNotification(string title, string content, string imageUrl);
        void RenderVideoNotification(string title, string content, string videoUrl);
    }

    public abstract class NotificationBase
    {
        protected string title;
        protected string content;

        public NotificationBase(string title, string content)
        {
            this.title = title;
            this.content = content;
        }

        public abstract void Render();
    }

    public class WebPlatform : IPlatform
    {
        public void RenderTextNotification(string title, string content)
        {
            Console.WriteLine($"[Web - HTML] <div class='notification'>");
            Console.WriteLine($"  <h3>{title}</h3>");
            Console.WriteLine($"  <p>{content}</p>");
            Console.WriteLine("</div>");
        }

        public void RenderImageNotification(string title, string content, string imageUrl)
        {
            Console.WriteLine($"[Web - HTML] <div class='notification-image'>");
            Console.WriteLine($"  <img src='{imageUrl}' />");
            Console.WriteLine($"  <h3>{title}</h3>");
            Console.WriteLine($"  <p>{content}</p>");
            Console.WriteLine("</div>");
        }

        public void RenderVideoNotification(string title, string content, string videoUrl)
        {
            Console.WriteLine($"[Web - HTML] <div class='notification-video'>");
            Console.WriteLine($"  <video src='{videoUrl}' controls></video>");
            Console.WriteLine($"  <h3>{title}</h3>");
            Console.WriteLine($"  <p>{content}</p>");
            Console.WriteLine("</div>");
        }
    }

    public class DesktopPlatform : IPlatform
    {
        public void RenderTextNotification(string title, string content)
        {
            Console.WriteLine($"[Desktop - Toast] Windows Notification:");
            Console.WriteLine($"╔══════════════════════════╗");
            Console.WriteLine($"║ {title.PadRight(24)} ║");
            Console.WriteLine($"║ {content.PadRight(24)} ║");
            Console.WriteLine($"╚══════════════════════════╝");
        }

        public void RenderImageNotification(string title, string content, string imageUrl)
        {
            Console.WriteLine($"[Desktop - Toast] Windows Notification with Image:");
            Console.WriteLine($"╔══════════════════════════╗");
            Console.WriteLine($"║ [IMG: {imageUrl.Substring(0, Math.Min(15, imageUrl.Length))}...]  ║");
            Console.WriteLine($"║ {title.PadRight(24)} ║");
            Console.WriteLine($"║ {content.PadRight(24)} ║");
            Console.WriteLine($"╚══════════════════════════╝");
        }

        public void RenderVideoNotification(string title, string content, string videoUrl)
        {
            Console.WriteLine($"[Desktop - Toast] Windows Notification with Video:");
            Console.WriteLine($"╔══════════════════════════╗");
            Console.WriteLine($"║ ▶ {videoUrl.Substring(0, Math.Min(20, videoUrl.Length))}... ║");
            Console.WriteLine($"║ {title.PadRight(24)} ║");
            Console.WriteLine($"║ {content.PadRight(24)} ║");
            Console.WriteLine($"╚══════════════════════════╝");
        }
    }

    public class MobilePlatform : IPlatform
    {
        public void RenderTextNotification(string title, string content)
        {
            Console.WriteLine($"[Mobile - Native] Push Notification:");
            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Body: {content}");
            Console.WriteLine($"Icon: notification_icon.png");
        }

        public void RenderImageNotification(string title, string content, string imageUrl)
        {
            Console.WriteLine($"[Mobile - Native] Rich Push Notification:");
            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Body: {content}");
            Console.WriteLine($"Image: {imageUrl}");
            Console.WriteLine($"Style: BigPictureStyle");
        }

        public void RenderVideoNotification(string title, string content, string videoUrl)
        {
            Console.WriteLine($"[Mobile - Native] Video Push Notification:");
            Console.WriteLine($"Title: {title}");
            Console.WriteLine($"Body: {content}");
            Console.WriteLine($"Video: {videoUrl}");
            Console.WriteLine($"Action: Tap to play");
        }
    }

    public class TextNotification(IPlatform platform, string title, string content)
        : NotificationBase(title, content)
    {
        private readonly IPlatform platform = platform;

        public override void Render() =>
            platform.RenderTextNotification(title, content);
    }

    public class ImageNotification(IPlatform platform, string title, string content, string imageUrl)
        : NotificationBase(title, content)
    {
        private readonly IPlatform platform = platform;
        private readonly string imageUrl = imageUrl;

        public override void Render() =>
            platform.RenderImageNotification(title, content, imageUrl);
    }

    public class VideoNotification(IPlatform platform, string title, string content, string videoUrl)
        : NotificationBase(title, content)
    {
        private readonly IPlatform platform = platform;
        private readonly string videoUrl = videoUrl;

        public override void Render() =>
            platform.RenderVideoNotification(title, content, videoUrl);
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Notificações Multi-Plataforma ===\n");

            // Problema: Precisamos de uma classe para cada combinação
            var platformWeb = new WebPlatform();
            var textWeb = new TextNotification(platformWeb, "Novo Pedido", "Você tem um novo pedido");
            textWeb.Render();
            Console.WriteLine();

            var platformMobile = new MobilePlatform();
            var textMobile = new TextNotification(platformMobile, "Novo Pedido", "Você tem um novo pedido");
            textMobile.Render();
            Console.WriteLine();

            var imageWeb = new ImageNotification(
                platformWeb,
                "Promoção",
                "50% de desconto!",
                "promo.jpg"
            );
            imageWeb.Render();
            Console.WriteLine();

            var videoMobile = new VideoNotification(
                platformMobile,
                "Tutorial",
                "Aprenda a usar o app",
                "tutorial.mp4"
            );
            videoMobile.Render();
            Console.WriteLine();

            Console.WriteLine("=== PROBLEMAS ===");
            Console.WriteLine("✗ Explosão de classes: 3 tipos × 3 plataformas = 9 classes");
            Console.WriteLine("✗ Código duplicado entre classes similares");
            Console.WriteLine("✗ Adicionar novo tipo = criar 3 classes (uma por plataforma)");
            Console.WriteLine("✗ Adicionar nova plataforma = criar 3 classes (uma por tipo)");
            Console.WriteLine("✗ As duas hierarquias (tipo e plataforma) estão fortemente acopladas");
            Console.WriteLine();

            // Perguntas para reflexão:
            // - Como separar a abstração (tipo de notificação) da implementação (plataforma)?
            // - Como adicionar novos tipos de notificação sem criar classes para cada plataforma?
            // - Como adicionar novas plataformas sem modificar os tipos existentes?
            // - Como evitar a explosão combinatória de classes?
        }
    }
}
