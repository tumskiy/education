public class Program
{
    public static void Main(string[] args)
    {      
        // GoSort();
        BubbleSort bubbleSorter = new BubbleSort();           
        InsertionSort insertionSorter = new InsertionSort();
        int[] array = Enumerable.Repeat(0, 5).Select(i => new Random().Next(0, 9)).ToArray();

        Result(array, bubbleSorter);
        Result(array, insertionSorter);
    }

    static void GoSort()
    {
        int[] array = Enumerable.Repeat(0, 5).Select(i => new Random().Next(0, 9)).ToArray();

        ISortable sorter = null;
        Console.WriteLine("0-BubbleSort\n1-InsertionSort");
        int type = Console.Read();

        if (type == 0) sorter = new BubbleSort();
        if (type == 1) sorter = new InsertionSort();


        int[]? arraySorted = sorter?.Sort(array) ?? new int[0];

        Console.WriteLine(string.Join(", ", array));
        Console.WriteLine(string.Join(", ", arraySorted));
    }

    static void Result(int[] array, ISortable sortable)
    {
        int[] arraySorted = sortable.Sort(array);
        Console.WriteLine(string.Join(", ", array));
        Console.WriteLine(string.Join(", ", arraySorted));
    }
    //получать массив и получать объект с помощью которого сортируется масссив
}


public interface ISortable
{
    public int[] Sort(int[] array);
}

class BubbleSort : ISortable
{
    public int[] Sort(int[] arraySource)
    { 
        int[] array = (int[])arraySource.Clone();
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = 0; j < array.Length - 1; j++)
            {
                if (array[j] > array[j + 1]) //если значение текущего инлекса больше следующего
                {
                    int temp = array[j + 1]; //временная переменная
                    array[j + 1] = array[j]; //смена мест
                    array[j] = temp; //смены следующего на текущий индекс
                }
            }
        }
        return array; //теперь значю зачем ретурн, для того, что бы дальше в Main использовать функцию сортировки
    }
}

class InsertionSort : ISortable
{
    public int[] Sort(int[] arraySource)
    {
        int[] arr = (int[])arraySource.Clone();
        for (var i = 1; i < arr.Length; i++) // цикл на длину основного массива (1)
        {
            int num = arr[i]; //текущий индекс цикла
            int j = i - 1; // индекс нового массива (2) равен предыдщему индексу основного масивва (1)
            int next = j + 1;
            while (next > 0 && arr[j] > num) // пока следующий элемент массива 2 больше текущего, то смещаем его индекс на 1
            {
                arr[j + 1] = arr[j]; //следующий элемент становится равен текущему
                j--;
            }
            arr[j + 1] = num; //следующий элемент становится равен текущему
        }
        return arr; //ретурн для использования переменной в будущем, а то не вызовется
    }
}