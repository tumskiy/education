interface ISort
{
    int[] bubblesort(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = 0; j < arr.Length - 1; j++)
            {
                if (arr[j] > arr[j + 1]) //если значение текущего инлекса больше следующего
                {
                    int temp = arr[j + 1]; //временная переменная
                    arr[j + 1] = arr[j]; //смена мест
                    arr[j] = temp; //смены следующего на текущий индекс
                }
            }
        }
        return arr; //теперь значю зачем ретурн, для того, что бы дальше в Main использовать функцию сортировки
    }
    int[] insertionsort(int[] arr)
    {
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
class Program
{
    static void Main(string[] args)
    { 
        int[] array = Enumerable.Repeat(0, 5).Select(i => new Random().Next(0, 9)).ToArray();
        ISorted? sorter = null;

        int type = Console.Read();

        if (type == 0) sorter = new // Объект с реализацией сортировки
        if (type == 1) sorter = new // Объект с реализацией сортировки
        if (type == 2) sorter = new // Объект с реализацией сортировки
                                //...

        int[]? arraySorted = sorter?.Sort(array) ?? new int[0];

        Console.WriteLine(string.Join(", ", array));
        Console.WriteLine(string.Join(", ", arraySorted));
    }
}