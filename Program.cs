
using System.Xml.Linq;

/**
 * Vrchol binarniho vyhledavaciho stromu (BST) se jmeny souboru
 */
class Node
{
    /** Klic - jmeno souboru */
    public String key;
    /** Levy potomek */
    public Node left;
    /** Pravy potomek */
    public Node right;

    /**
	 * Vytvori novy vrchol binarniho vyhledavaciho stromu
	 * @param key klic - jmeno souboru
	 */
    public Node(String key)
    {
        this.key = key;
    }
}

/**
 * Binarni vyhledavaci strom se jmeny souboru
 */
class BinarySearchTree
{
    public int matchesFound;
    /** Koren binarniho vyhledavaciho stromu */
    Node root;

    /**
	 * Prida do BST prvek se zadanym klicem - jmenem souboru
	 */
    public void Add(String key)
    {
        if (root == null)
        {
            root = new Node(key);
        }
        else
        {
            AddUnder(root, key);
        }
    }

    /**
	 * Vlozi pod zadany vrchol novy vrchol se zadanym klicem
	 */
    void AddUnder(Node n, String key)
    {
        if (String.Compare(key, n.key, true) < 0)
        { // porovna retezce bez ohledu na velikost pismen
          // vrchol patri doleva, je tam misto?
            if (n.left == null)
            {
                n.left = new Node(key);
            }
            else
            {
                AddUnder(n.left, key);
            }
        }
        else
        {
            // vrchol patri doprava, je tam misto?
            if (n.right == null)
            {
                n.right = new Node(key);
            }
            else
            {
                AddUnder(n.right, key);
            }
        }
    }

    /// <summary>
    /// Metoda určí, zda se ve stromě nachází řetězec <b>key</b>.
    /// </summary>
    /// <param name="key">Hledaný řetězec</param>
    /// <returns>True, pokud obsahuje řetězec, jinak false</returns>
    public bool Contains(String key)
    {
        Node n = root;

        while (n != null)
        {
            if (n.key == key)
            {
                return true;
            }
            if (String.Compare(key, n.key, true) < 0)
            {
                n = n.left;
            }
            else
            {
                n = n.right;
            }
        }
        return false;
    }

    /// <summary>
    /// Metoda určí, zda se ve stromě nachází řetězec <b>key</b>. Používá rekurzi.
    /// </summary>
    /// <param name="key">Hledaný řetězec</param>
    /// <returns>True, pokud obsahuje řetězec, jinak false</returns>
    public bool ContainsWithRec(String key)
    {
        Node n = root;

        return ContainsRec(n, key);
    }

    /// <summary>
    /// Pomocná metoda k metodě <seealso cref="ContainsWithRec(string)"/>
    /// </summary>
    /// <param name="n">Aktuální prvek</param>
    /// <param name="key">Hledaný řetězec</param>
    /// <returns>True, pokud obsahuje řetězec, jinak false</returns>
    public bool ContainsRec(Node n,String key)
    {
        if(n != null)
        {
            if (n.key == key)
            {
                return true;
            }
            if (String.Compare(key, n.key, true) < 0)
            {
                return ContainsRec(n.left, key);
            }
            else
            {
                return ContainsRec(n.right,key);
            }
        }
        return false;
    }

    /// <summary>
    /// Metoda vypíše do konzole všechny řetězce obsažené v BST v abecedním pořadí pomocí <b>inorder</b>průchodu.
    /// </summary>
    public void PrintSorted()
    {
        Node n = root;
        PrintSortedR(n);
    }

    /// <summary>
    /// Pomocná metoda k metodě <seealso cref="PrintSorted"/>
    /// </summary>
    /// <param name="n">Aktuální prvek</param>
    public void PrintSortedR(Node n)
    {
        if (n != null)
        {
            PrintSortedR(n.left);
            Console.WriteLine(n.key);
            PrintSortedR(n.right);
        }
    }

    /// <summary>
    /// Metoda vypíše do konzole všechny řetězce obsažené v BST začínající předponou <b>prefix</b>.
    /// </summary>
    /// <param name="prefix">Hledaný prefix</param>
    /// <returns>Vrací celkový počet prohledaných vrcholů</returns>
    public int PrintAllStartingWith(String prefix)
    {
        Node n = root;
        matchesFound = 0;
        int count = 0;
        count = PrintAllStartingWithR(n, prefix, count);

        return count;
    }

    /// <summary>
    /// Pomocná metoda k metodě <seealso cref="PrintAllStartingWith(string)"/>.
    /// </summary>
    /// <param name="n">Aktuální prvek</param>
    /// <param name="prefix">Hledaný prefix</param>
    /// <param name="count">Aktuální počet prohledaných vrcholů</param>
    /// <returns>Vrací celkový počet prohledaných vrcholů</returns>
    public int PrintAllStartingWithR(Node n, String prefix, int count)
    {
        if (n != null) 
        {
            String sub = n.key.Substring(0, prefix.Length);

            count++;

            if (prefix.Equals(sub))
            {
                Console.WriteLine(n.key);
                matchesFound++;
                count = PrintAllStartingWithR(n.left, prefix, count);
                count = PrintAllStartingWithR(n.right, prefix, count);
            }
            else
            {
                if (String.Compare(prefix, sub, true) < 0)
                    count = PrintAllStartingWithR(n.left, prefix, count);

                if (String.Compare(prefix, sub, true) > 0)
                    count = PrintAllStartingWithR(n.right, prefix, count);
            }
        }
        return count;
    }

    /// <summary>
    /// Metoda umožní vyjmout z datové struktury vrchol s klíčem <b>key</b>.
    /// </summary>
    /// <param name="key">Klíč odebíraného prvku</param>
    public void Remove(String key)
    {
        Node n = root;
        Node pred = null;
        while (!key.Equals(n.key))
        {
            pred = n;
            if (key.CompareTo(n.key) < 0)
                n = n.left;
            else n = n.right;
        }
        if ((n.left == null) || (n.right == null))
        {
            Node replacement = n.left;
            if (n.right != null)
                replacement = n.right;
            if (pred == null)
                root = replacement;
            else
            if (pred.left == n)
                pred.left = replacement;
            else pred.right = replacement;
        }
        else
        {
            Node leftMax = n.left;
            Node leftMaxPred = n;
            while (leftMax.right != null)
            {
                leftMaxPred = leftMax;
                leftMax = leftMax.right;
            }
            n.key = leftMax.key;
            if (leftMax != n.left)
                leftMaxPred.right = leftMax.left;
            else
                n.left = leftMax.left;
        }
    }
}
/**
    * Trida pro doplnovani textu na zaklade historie
    */
public class Autocomplete
{
    public static void Main(String[] args)
    {
        /*
        BinarySearchTree bst = new BinarySearchTree();
        bst.Add("http://portal.zcu.cz");
        bst.Add("http://yaurshhhhhheware.zcu.cz");
        bst.Add("http://zourseware.zcu.cz");
        bst.Add("http://ybuasdasdrseware.zcu.cz");
        bst.Add("http://ybausdasdrseware.zcu.cz");
        bst.Add("http://courseware.zcu.cz");
        bst.Add("http://couzseware.zcu.cz");
        bst.Add("http://pobcseware.zcu.cz");
        //bst.Add("http://yourseware.zcu.cz");
        //bst.Add("http://yeurseware.zcu.cz");
        Console.WriteLine(bst.PrintAllStartingWith("http://po"));
        */

        BinarySearchTree t = new BinarySearchTree();
        StreamReader sr = new StreamReader("requests.txt");

        int nodesCount = 0;
        string line = "";
        while ((line = sr.ReadLine()) != null)
        {
            String[] lineArr = line.Split(" ");
            switch (lineArr[0].Trim())
            {
                case "A":
                    t.Add(lineArr[1].Trim());
                    nodesCount++;
                    break;
                case "R":
                    t.Remove(lineArr[1].Trim());
                    nodesCount--;
                    break;
                case "P":
                    int num = t.PrintAllStartingWith(lineArr[1].Trim());
                    Console.WriteLine($"{nodesCount}/{num}/{t.matchesFound}");
                    break;
            }
        }
   
    }
}