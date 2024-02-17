using System.Reflection.Metadata.Ecma335;

namespace Tree
{
    class TreeNode<T>
    {
        public T Data { get; set; }
        public List<TreeNode<T>> Children { get; set; } = new List<TreeNode<T>>();
    }


    internal class Program
    {
        static TreeNode<String> MakeTree()
        {
            TreeNode<String> root = new TreeNode<String>() { Data = "R1개발실" };
            {
                {
                    TreeNode<String> node = new TreeNode<String>() { Data = "디자인팀" };
                   /* node.Children.Add(new TreeNode<String>() { Data = "전투" });
                    node.Children.Add(new TreeNode<String>() { Data = "경제" });
                    node.Children.Add(new TreeNode<String>() { Data = "스토리" });*/
                    root.Children.Add(node);

                }
                {
                    TreeNode<String> node = new TreeNode<String>() { Data = "프로그래밍팀" };
                    node.Children.Add(new TreeNode<String>() { Data = "서버" });
                    node.Children.Add(new TreeNode<String>() { Data = "클라" });
                    node.Children.Add(new TreeNode<String>() { Data = "엔진" });
                    root.Children.Add(node);

                }
                {
                    TreeNode<String> node = new TreeNode<String>() { Data = "아트팀" };
                    //node.Children.Add(new TreeNode<String>() { Data = "배경" });
                    //node.Children.Add(new TreeNode<String>() { Data = "캐릭터" });
                    root.Children.Add(node);

                }
            }
            return root;
        }

        static void PrintTree(TreeNode<String> root)
        {
            Console.WriteLine(root.Data);
            foreach (TreeNode<String> child in root.Children)
                    PrintTree(child);
        }

        static int GetHeight(TreeNode<String> root)
        {
            int height = 0;

            foreach (TreeNode<String> child in root.Children)
            {
                int newHeight = GetHeight(child) + 1;
               
                height = Math.Max(height, newHeight);
            }
            return height;
        }

        static void Main(string[] args)
        {
            TreeNode<String> root= MakeTree();
            PrintTree(root);
            
            Console.WriteLine(GetHeight(root));
        }
    }
}
