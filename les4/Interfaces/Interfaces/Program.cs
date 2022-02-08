interface ISorted
{
    void Sorted();
}

class Sort : ISorted
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
    int[] selectionsort(int[] arr)
    {
        int min, temp;
        int length = arr.Length;
        for (int i = 0; i < length - 1; i++)
        {
            min = i;
            for (int j = i + 1; j < length; j++)
            {
                if (arr[j] < arr[min])
                    min = j;
            }
            if (min != i)
            {
                temp = arr[i];
                arr[i] = arr[min];
                arr[min] = temp;
            }
        }
        return arr;
    } //не мое
    int[] arrsort(int[] name)
    {
        int b = 0;
        int left = 0;//Левая граница
        int right = name.Length - 1;//Правая граница
        while (left < right)
        {
            for (int i = left; i < right; i++)//Слева направо...
            {
                if (name[i] > name[i + 1])
                {
                    b = name[i];
                    name[i] = name[i + 1];
                    name[i + 1] = b;
                    b = i;
                }
            }
            right = b;//Сохраним последнюю перестановку как границу
            if (left >= right) break;//Если границы сошлись выходим
            for (int i = right; i > left; i--)//Справа налево...
            {
                if (name[i - 1] > name[i])
                {
                    b = name[i];
                    name[i] = name[i - 1];
                    name[i - 1] = b;
                    b = i;
                }
            }
            left = b;//Сохраним последнюю перестановку как границу
        }
        return name;
    }//не мое
    int[] bucketsort(ref int[] items)
    {
        int maxValue = items[0];
        int minValue = items[0];

        for (int i = 1; i < items.Length; i++)
        {
            if (items[i] > maxValue)
                maxValue = items[i];

            if (items[i] < minValue)
                minValue = items[i];
        }

        List<int>[] bucket = new List<int>[maxValue - minValue + 1];

        for (int i = 0; i < bucket.Length; i++)
        {
            bucket[i] = new List<int>();
        }

        for (int i = 0; i < items.Length; i++)
        {
            bucket[items[i] - minValue].Add(items[i]);
        }

        int position = 0;
        for (int i = 0; i < bucket.Length; i++)
        {
            if (bucket[i].Count > 0)
            {
                for (int j = 0; j < bucket[i].Count; j++)
                {
                    items[position] = bucket[i][j];
                    position++;
                }
            }
        }
        return items;
    }//не мое

}


class Program
{
    static void Main(string[] args)
    {
        int[] array = Enumerable
        .Repeat(0, 5)
        .Select(i => new Random().Next(0, 9))
        .ToArray();
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