using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Dtree
{
    public interface ITree<T>
    {
        T Root();                                //求树的根结点
        T Parent(T t);                           //求结点t的双亲结点
        T Child(T t, int i);                     //求结点t的第i个子结点
        T RightSibling(T t);                     //求结点t第一个右边兄弟结点
        bool Insert(T s, T t, int i);            //将树s加入树中作为结点t的第i颗子树
        T Delete(T t, int i);                    //删除结点t的第i颗子树
        void Traverse(int TraverseType);         //按某种方式遍历树
        void Clear();                            //清空树
        bool IsEmpty();                          //判断是否为空
        int GetDepth(T t);                          //求树的深度
    }


    /// <summary>
    /// 循环顺序队列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CSeqQueue<T>
    {
        private int maxsize;       //循环顺序队列的容量
        private T[] data;          //数组，用于存储循环顺序队列中的数据元素
        private int front;         //指示最近一个已经离开队列的元素所占有的位置 循环顺序队列的对头
        private int rear;          //指示最近一个进入队列的元素的位置           循环顺序队列的队尾

        public T this[int index]
        {
            get { return data[index]; }
            set { data[index] = value; }
        }

        //容量属性
        public int Maxsize
        {
            get { return maxsize; }
            set { maxsize = value; }
        }

        //对头指示器属性
        public int Front
        {
            get { return front; }
            set { front = value; }
        }

        //队尾指示器属性
        public int Rear
        {
            get { return rear; }
            set { rear = value; }
        }

        public CSeqQueue()
        {

        }

        public CSeqQueue(int size)
        {
            data = new T[size];
            maxsize = size;
            front = rear = -1;
        }

        //判断循环顺序队列是否为满
        public bool IsFull()
        {
            if ((front == -1 && rear == maxsize - 1) || (rear + 1) % maxsize == front)
                return true;
            else
                return false;
        }

        //清空循环顺序列表
        public void Clear()
        {
            front = rear = -1;
        }

        //判断循环顺序队列是否为空
        public bool IsEmpty()
        {
            if (front == rear)
                return true;
            else
                return false;
        }

        //入队操作
        public void EnQueue(T elem)
        {
            if (IsFull())
            {
                //  Console.WriteLine("Queue is Full !");
                return;
            }
            rear = (rear + 1) % maxsize;
            data[rear] = elem;
        }

        //出队操作
        public T DeQueue()
        {
            if (IsEmpty())
            {
                //      Console.WriteLine("Queue is Empty !");
                return default(T);
            }
            front = (front + 1) % maxsize;
            return data[front];
        }

        //获取对头数据元素
        public T GetFront()
        {
            if (IsEmpty())
            {
                //      Console.WriteLine("Queue is Empty !");
                return default(T);
            }
            return data[(front + 1) % maxsize];//front从-1开始
        }

        //求循环顺序队列的长度
        public int GetLength()
        {
            return (rear - front + maxsize) % maxsize;
        }
    }

    /// <summary>
    /// 树的多链表结点类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MLNode<T>
    {
        private T data;                   //存储结点的数据
        private MLNode<T>[] childs;       //存储子结点的位置

        public MLNode(int max)
        {
            childs = new MLNode<T>[max];
            for (int i = 0; i < childs.Length; i++)
            {
                childs[i] = null;
            }
        }

        public T Data
        {
            get { return data; }
            set { data = value; }
        }

        public MLNode<T>[] Childs
        {
            get { return childs; }
            set { childs = value; }
        }
    }



    public class MLTree<T> : ITree<MLNode<T>>
    {
        private MLNode<T> head;

        public MLNode<T> Head
        {
            get { return head; }
            set { head = value; }
        }

        public MLTree()
        {
            head = null;
        }

        public MLTree(MLNode<T> node)
        {
            head = node;
        }

        //求树的根结点
        public MLNode<T> Root()
        {
            return head;
        }

        public void Clear()
        {
            head = null;
        }

        //待测试！！！
        public int GetDepth(MLNode<T> root)
        {
            int len;
            if (root == null)
            {
                return 0;
            }
            for (int i = 0; i < root.Childs.Length; i++)
            {
                if (root.Childs[i] != null)
                {
                    len = GetDepth(root.Childs[i]);
                    return len + 1;
                }
            }
            return 0;
        }

        public bool IsEmpty()
        {
            return head == null;
        }

        //求结点t的双亲结点，如果t的双亲结点存在，返回双亲结点，否则返回空
        //按层序遍历的算法进行查找
        public MLNode<T> Parent(MLNode<T> t)
        {
            MLNode<T> temp = head;
            if (IsEmpty() || t == null) return null;
            if (temp.Data.Equals(t.Data)) return null;
            CSeqQueue<MLNode<T>> queue = new CSeqQueue<MLNode<T>>(50);
            queue.EnQueue(temp);
            while (!queue.IsEmpty())
            {
                temp = (MLNode<T>)queue.DeQueue();
                for (int i = 0; i < temp.Childs.Length; i++)
                {
                    if (temp.Childs[i] != null)
                    {
                        if (temp.Childs[i].Data.Equals(t.Data))
                        {
                            return temp;
                        }
                        else
                        {
                            queue.EnQueue(temp.Childs[i]);
                        }
                    }
                }
            }
            return null;
        }

        //求结点t的第i个子结点。如果存在，返回第i个子结点，否则返回空
        //i=0时，表示求第一个子节点
        public MLNode<T> Child(MLNode<T> t, int i)
        {
            if (t != null && i < t.Childs.Length)
            {
                return t.Childs[i];
            }
            else
            {
                return null;
            }
        }

        //求结点t第一个右边兄弟结点，如果存在，返回第一个右边兄弟结点，否则返回空
        public MLNode<T> RightSibling(MLNode<T> t)
        {
            MLNode<T> pt = Parent(t);
            if (pt != null)
            {
                int i = FindRank(t);
                return Child(pt, i + 1);
            }
            else
            {
                return null;
            }
        }

        //查找结点t在兄弟中的排行，成功时返回位置，否则返回-1
        private int FindRank(MLNode<T> t)
        {
            MLNode<T> pt = Parent(t);
            for (int i = 0; i < pt.Childs.Length; i++)
            {
                MLNode<T> temp = pt.Childs[i];
                if (temp != null && temp.Data.Equals(t.Data))
                {
                    return i;
                }
            }
            return -1;
        }

        //查找在树中的结点t，成功是返回t的位置，否则返回null
        private MLNode<T> FindNode(MLNode<T> t)
        {
            if (head.Data.Equals(t.Data)) return head;
            MLNode<T> pt = Parent(t);
            foreach (MLNode<T> temp in pt.Childs)
            {
                if (temp != null && temp.Data.Equals(t.Data))
                {
                    return temp;
                }
            }
            return null;
        }

        //把以s为头结点的树插入到树中作为结点t的第i颗子树。成功返回true
        public bool Insert(MLNode<T> s, MLNode<T> t, int i)
        {
            if (IsEmpty()) head = t;
            t = FindNode(t);
            if (t != null && t.Childs.Length > i)
            {
                t.Childs[i] = s;
                return true;
            }
            else
            {
                return false;
            }
        }

        //删除结点t的第i个子树。返回第i颗子树的根结点。
        public MLNode<T> Delete(MLNode<T> t, int i)
        {
            t = FindNode(t);
            MLNode<T> node = null;
            if (t != null && t.Childs.Length > i)
            {
                node = t.Childs[i];
                t.Childs[i] = null;
            }
            return node;
        }


        //先序遍历
        //根结点->遍历根结点的左子树->遍历根结点的右子树 
        public void preorder(MLNode<T> root)
        {
            if (root == null)
                return;
            //    Console.WriteLine(root.Data + " ");
            for (int i = 0; i < root.Childs.Length; i++)
            {
                preorder(root.Childs[i]);
            }
        }


        //后序遍历
        //遍历根结点的左子树->遍历根结点的右子树->根结点
        public void postorder(MLNode<T> root)
        {
            if (root == null)
            { return; }
            for (int i = 0; i < root.Childs.Length; i++)
            {
                postorder(root.Childs[i]);
            }
            //   Console.WriteLine(root.Data + " ");
        }


        //层次遍历
        //引入队列 
        public void LevelOrder(MLNode<T> root)
        {
            //     Console.WriteLine("遍历开始：");
            if (root == null)
            {
                //         Console.WriteLine("没有结点！");
                return;
            }

            MLNode<T> temp = root;

            CSeqQueue<MLNode<T>> queue = new CSeqQueue<MLNode<T>>(50);
            queue.EnQueue(temp);
            while (!queue.IsEmpty())
            {
                temp = (MLNode<T>)queue.DeQueue();
                //       Console.WriteLine(temp.Data + " ");
                for (int i = 0; i < temp.Childs.Length; i++)
                {
                    if (temp.Childs[i] != null)
                    {
                        queue.EnQueue(temp.Childs[i]);
                    }
                }
            }
            //      Console.WriteLine("遍历结束！");
        }

        //按某种方式遍历树
        //0 先序
        //1 后序
        //2 层序
        public void Traverse(int TraverseType)
        {
            if (TraverseType == 0) preorder(head);
            else if (TraverseType == 1) postorder(head);
            else LevelOrder(head);
        }
    }

   
    public class operation
    {
        public static int K_cnt = 0;
        public static MLNode<string> getstring2tree(string a, App1.Popup m_popup)
        {
            char[] charArray = a.ToCharArray();
            MLTree<string> tree = new MLTree<string>();
            string sub;
            int i, j = 2, k = 0, cnt = 0, m;
           
            bool ok;
            MLNode<string> p0;
            for (i = j; charArray[i] != '&'; i++) { }
            sub = a.Substring(j, i - j);
            m_popup.changetxt(sub);
            MLNode<string> pt = new MLNode<string>(10);
            pt.Data = sub;
            string s_temp, s_full;
            while (charArray[j] != 'E')
            {
                j = i + 2;
                MLNode<string> ct = new MLNode<string>(40);
                ct.Data = " &" + charArray[i+1];
                ok = tree.Insert(ct, pt, k);
                k = k + 1;
                cnt = 0;
                string S_xinzhi;
                switch (charArray[i+1])
                {
                    case '1':
                        S_xinzhi = "prop. ";
                        break;
                    case '2':
                        S_xinzhi = "int. ";
                        break;
                    case '3':
                        S_xinzhi = "abbr. ";
                        break;
                    case '4':
                        S_xinzhi = "n. ";
                        break;
                    case '5':
                        S_xinzhi = "v. ";
                        break;
                    case '6':
                        S_xinzhi = "adj. ";
                        break;
                    case '7':
                        S_xinzhi = "pron. ";
                        break;
                    case '8':
                        S_xinzhi = "art. ";
                        break;
                    case '9':
                        S_xinzhi = "na. ";
                        break;
                    default:
                        S_xinzhi = "more. ";
                        break;
                }
                s_temp = S_xinzhi;
                s_full = S_xinzhi;
                for (i = j; charArray[i] != '&'; i++)
                {
                    if (charArray[i] == '$' && charArray[i + 1] == '$')
                    {
                        sub = a.Substring(j, i - j);
                        p0 = new MLNode<string>(1);
                        p0.Data = sub;
                        ok = tree.Insert(p0, ct, cnt);
                        i = i + 1;
                        j = i + 1;
                        cnt = cnt + 1;
                        if (cnt <= 4)
                        {
                            s_temp = s_temp + sub + ';';
                        }
                        s_full = s_full + sub + ';';
                        //     if (cnt % 4 == 0)
                        //        s_full = s_full + '\n';
                        //   if (cnt == 4)
                        //      m_popup.addbutton(s_temp,k-1);
                        if (cnt == 38)
                            break;
                    }
                    if (charArray[i] == 'E')
                        break;
                }
                if (cnt == 38)
                {
                    m_popup.addbutton(s_full, s_temp, k - 1);
                    while (charArray[j] < '0' || charArray[j] > '9')
                    {
                        if (charArray[j] == 'E')
                            break;
                        j++;
                    }
                    i = j;
                    if (charArray[j] == 'E')
                        break;
                }
                else
                {
                    sub = a.Substring(j, i - j);
                    p0 = new MLNode<string>(1);
                    p0.Data = sub;
                    ok = tree.Insert(p0, ct, cnt);
                    cnt = cnt + 1;
                    if (cnt <= 4)
                        s_temp = s_temp + sub + ';';
                    s_full = s_full + sub + ';';
                    //     if (cnt % 4 == 0)
                    //         s_full = s_full + '\n';
                    //    if (cnt <= 4)
                    m_popup.addbutton(s_full, s_temp, k - 1);
                    if (charArray[i] == 'E')
                        break;
                }
            }
            K_cnt = k;
            return pt;
            /*
        for (i = j; charArray[i] < '0' || charArray[i] > '9'; i++) { }
        sub = a.Substring(j, i - j);
        m_popup.changetxt(sub);
        MLNode<string> pt = new MLNode<string>(10);
        pt.Data = sub;
        string s_temp,s_full;
        while (charArray[j] != 'E')
        {
            j = i + 1;
            MLNode<string> ct = new MLNode<string>(40);
            ct.Data = " " + charArray[i];
            ok = tree.Insert(ct, pt, k);
            k = k + 1;
            cnt = 0;
            string S_xinzhi;
            switch (charArray[i])
            {
                case '1':
                    S_xinzhi= "prop. ";
                    break;
                case '2':
                    S_xinzhi = "int. ";
                    break;
                case '3':
                    S_xinzhi = "abbr. ";
                    break;
                case '4':
                    S_xinzhi = "n. ";
                    break;
                case '5':
                    S_xinzhi = "v. ";
                    break;
                case '6':
                    S_xinzhi = "adj. ";
                    break;
                case '7':
                    S_xinzhi = "pron. ";
                    break;
                case '8':
                    S_xinzhi = "art. ";
                    break;
                case '9':
                    S_xinzhi = "na. ";
                    break;
                default:
                    S_xinzhi = "more. ";
                    break;
            }
            s_temp = S_xinzhi;
            s_full = S_xinzhi;
            for (i = j; charArray[i] < '0' || charArray[i] > '9'; i++)
            {
                if (charArray[i] == '$' && charArray[i + 1] == '$')
                {
                    sub = a.Substring(j, i - j);
                    p0 = new MLNode<string>(1);
                    p0.Data = sub;
                    ok = tree.Insert(p0, ct, cnt);
                    i = i + 1;
                    j = i + 1;
                    cnt = cnt + 1;
                    if (cnt <= 4)
                    {
                        s_temp = s_temp + sub + ';';
                    }
                    s_full = s_full + sub + ';';
               //     if (cnt % 4 == 0)
                //        s_full = s_full + '\n';
                    //   if (cnt == 4)
                    //      m_popup.addbutton(s_temp,k-1);
                    if (cnt == 38)
                        break;
                }
                if (charArray[i] == 'E')
                    break;
            }
            if (cnt == 38)
            {
                m_popup.addbutton(s_full, s_temp, k - 1);
                while (charArray[j] < '0' || charArray[j] > '9')
                {
                    if (charArray[j] == 'E')
                        break;
                    j++;
                }
                i = j;
                if (charArray[j] == 'E')
                    break;
            }
            else
            {
                sub = a.Substring(j, i - j);
                p0 = new MLNode<string>(1);
                p0.Data = sub;
                ok = tree.Insert(p0, ct, cnt);
                cnt = cnt + 1;
                if (cnt <= 4)
                    s_temp = s_temp + sub + ';';
                s_full = s_full + sub + ';';
                //     if (cnt % 4 == 0)
                //         s_full = s_full + '\n';
                //    if (cnt <= 4)
                m_popup.addbutton(s_full, s_temp, k - 1);
                if (charArray[i] == 'E')
                    break;
            }
        }
        K_cnt = k;
        return pt;
        */
        }

        public static int getmax_index(int[] array)
        {
            int maxIndex = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[maxIndex] < array[i])
                    maxIndex = i;
            }
            return maxIndex;
        }

        public static string gettree2string(MLNode<string> pt)
        {
            string a = "00" + pt.Data;
            MLNode<string> p0;
            if (pt == null)
                return "error";
            int[] array = new int[10];
            int[] array_temp = App1.Popup.tap_;
            int k;
            for (k=0;k< App1.Popup.tap_.Length;k++)
            {
                array_temp[k] = App1.Popup.tap_[k];
            }
            for (k = 0; k < array_temp.Length; k++)
            {
                array[k] = getmax_index(array_temp);
                if (array_temp[array[k]] == 0)
                    break;
                array_temp[array[k]] = 0;
            }
            int i, j;
            for (i = 0; i<K_cnt && pt.Childs[array[i]] != null; i++)
            {
                p0 = pt.Childs[array[i]];
                a = a + p0.Data[1]+p0.Data[2];
        
                    for (j = 0; p0.Childs[j + 1] != null; j++)
                    {
                        a = a + p0.Childs[j].Data + "$$";
                    }
                    a = a + p0.Childs[j].Data;
         
            }
            a = a + 'E';
            return a;
        }

        public static void addmeaning(MLNode<string> pt,string s)
        {
            int i, j;
            bool ok;
            bool flag_0 = false;
            MLTree<string> tree = new MLTree<string>();
            MLNode<string> p0 = new MLNode<string>(1);
     //       p0.Data =s;
            for (i = 0; pt.Childs[i] != null; i++)
            {
                if (pt.Childs[i].Data == " &0")
                {
                    flag_0 = true;
                    for (j = 0; pt.Childs[i].Childs[j] != null; j++)
                    { }
                    if (j > 0)
                    {
                        string ch_tem=pt.Childs[i].Childs[0].Data;
                        pt.Childs[i].Childs[0].Data = s;
                        p0.Data = ch_tem;
                    }
                    ok = tree.Insert(p0, pt.Childs[i], j);
                    App1.Popup.tap_[i] += 5;
                }
            }
            if (flag_0 == false)
            {
                for (i = 0; pt.Childs[i] != null; i++) { }
                MLNode<string> p1 = new MLNode<string>(10);
                p1.Data = " &0";
                ok = tree.Insert(p1, pt, i);
                p0.Data = s;
                ok = tree.Insert(p0, p1, 0);
                K_cnt = K_cnt + 1;
                App1.Popup.tap_[i] = 5;
            }

        }

        public static void uprank(MLNode<string> pt, int i, int k)
        {
            MLNode<string> p0, p1, p_temp;
            string q0, q1, q_temp;
            int j;
            if (pt.Childs[i] != null)
            {
                p0 = pt.Childs[i];
                for (j = 0; pt.Childs[i].Childs[j] != null; j++) { }

                if (k<j)
                {
                    for (j = k; p0.Childs[j] != null; j++)
                    {
                        q0 = p0.Childs[j].Data;
                        q1 = p0.Childs[j - 4 > 0 ? j - 4 : 0].Data;
                        q_temp = q1;
                        p0.Childs[j - 4 > 0 ? j - 4 : 0].Data = q0;
                        p0.Childs[j].Data = q_temp;
                    }
                }
           /*     if (i > 0)
                {
                    p0 = pt.Childs[i];
                    p1 = pt.Childs[i - 4 > 0 ? i - 4 : 0];
                    p_temp = p0;
                    pt.Childs[i] = p1;
                    pt.Childs[i - 4 > 0 ? i - 4 : 0] = p_temp;
                }*/
            }
        }

        public static void clear(MLNode<string> pt)
        {
            int i, j;
            for (i = 0; pt.Childs[i] != null; i++)
            {
                for (j = 0; pt.Childs[i].Childs[j] != null; j++)
                {
                    pt.Childs[i].Childs[j] = null;
                }

            }
            pt.Childs[i] = null;
        }
    }
}


