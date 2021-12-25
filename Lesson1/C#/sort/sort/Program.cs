class Program
{
    static void swap(ref int elem1, ref int elem2) // создаем функцию сортировки с использованием третьего элемента
    {
        var temp = elem1;
        elem1 = elem2;
        elem2 = temp;
    }
    static int[] bubblesort(int[] arr) 
    {
        var len = arr.Length; //длина массива
        for (var i = 1; i < len; i++) //цикл длины массива меньше i
        {
            for (var j = 0; j < len - i; j++) //цикл длины массива меньше j
            {
                if (arr[j] > arr[j + 1]) //если элемент массива от j больше следующего элемента масства
                {
                    swap(ref arr[j], ref arr[j + 1]); //меняем их местами
                }
            }
        }

        return arr; //хз зачем ретурн, но без него не работает
    }

    //ввод массива с клавиатуры
    static void Main(string[] args)
    {   //пользователь задает количество эоементов массива
        Console.WriteLine("Введите количество элементов масива"); 
        int elemArr = Convert.ToInt32(Console.ReadLine());
        //пользователь задает значения элементов
        Console.WriteLine("Введите элемент массива (в 'int') для сортировки:");

        int[] mas = new int [elemArr];
        for (int i = 0; i < mas.Length; i++)
        {
            mas[i] = Convert.ToInt32(Console.ReadLine()); //преобразовавываем написанное с клавиатуры в 'int'
        }
        //сама сортировка с помощью метода 'bubblesort'
        bubblesort(mas);

        Console.WriteLine("вывод массива после сортировки:");
        for (int i = 0; i < mas.Length; i++)
        {
            Console.Write($"{mas[i]}, ",""); //выводим элементы массива через запятую
        }        
        Console.ReadLine();
    }
}

