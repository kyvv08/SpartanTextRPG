
namespace SpartanTextRPG
{
    internal interface IDescribable
    {
        string name { get; }
        void ViewInfo();
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            GameManager gmInstance = GameManager.Instance;
            gmInstance.StartGame();
            gmInstance.EnterVillage();
        }



    }
}
