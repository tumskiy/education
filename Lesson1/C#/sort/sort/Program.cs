class Program
{
    static int[] bubblesort(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = 0; j < arr.Length - 1; j++)
            {
                if (arr[j] > arr[j + 1]) //если значение текущего инлекса больше следующего
                {
                    var temp = arr[j + 1]; //временная переменная
                    arr[j + 1] = arr[j]; //смена мест
                    arr[j] = temp; //смены следующего на текущий индекс
                }
            }
        }
        return arr; //теперь значю зачем ретурн, для того, что бы дальше в Main использовать функцию сортировки
    }
    //ввод массива с клавиатуры
    static void Main(string[] args)
    {   //пользователь задает количество эоементов массива
        Console.WriteLine("Введите количество элементов масива");
        int elemArr = Convert.ToInt32(Console.ReadLine());
        //пользователь задает значения элементов
        Console.WriteLine("Введите элемент массива (в 'int') для сортировки:");

        int[] mas = new int[elemArr];
        for (int i = 0; i < mas.Length; i++)
        {
            mas[i] = Convert.ToInt32(Console.ReadLine()); //преобразовавываем написанное с клавиатуры в 'int'
        }
        //сама сортировка с помощью метода 'bubblesort'
        bubblesort(mas);
        Console.WriteLine("вывод массива после сортировки:");
        Console.Write(String.Join("/", mas)); //выводим элементы массива через запятую

        Console.ReadLine();
    }
}