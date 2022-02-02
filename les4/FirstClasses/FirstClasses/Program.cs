public class Sort
{
    public int[] bubblesort(int[] arr)
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

    /*public int[] mergesort (int[] array)
        {
            if (array.Length > 1) //если длина массива больше 1
            {
                var mid = Math.Round(array.Length / 2); //середина
                var LeftHalf = mergesort(array.Slice(0, mid)); // левая часть
                var RightHalf = mergesort(array.Slice(mid)); // правая часть

                var leftIndex = 0; // все три индекса по 0
                var rightIndex = 0;
                var index = 0;
                while (leftIndex < LeftHalf.Length && rightIndex < RightHalf.Length) //пока левый индекс меньше длины левой половины и правый индекс меньше длины правой половины
                {
                    if (LeftHalf[leftIndex] < RightHalf[rightIndex]) //если левый индекс левой половины меньше правого индекса правой половины
                    {
                        array[index] = LeftHalf[leftIndex]; //индекс ориг.масиивая становится равным левого индекса левого масиива
                        leftIndex++; //сдвигаеися
                    }
                    else
                    {
                        array[index] = RightHalf[leftIndex]; //ну тут элс, все понтяно
                        rightIndex++;
                    }
                    index++;
                }
                while (leftIndex < LeftHalf.Length) //пока индекс левого масиива меньше длины этого масиива
                {
                    array[index] = LeftHalf[leftIndex]; //индекс изначального масиива равен индексу левого масиива
                    leftIndex++;
                    index++;
                }
                while (rightIndex < RightHalf.Length) // и наоборот
                {
                    array[index] = RightHalf[rightIndex];
                    rightIndex++;
                    index++;
                }
            }
            return array;
        }
    }
    */
    public int[] insertionsort(int[] arr)
    {
        for (var i = 1; i < arr.Length; i++) // цикл на длину основного массива (1)
        {
            var num = arr[i]; //текущий индекс цикла
            var j = i - 1; // индекс нового массива (2) равен предыдщему индексу основного масивва (1)
            var next = j + 1;
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
    public static void Main(string[] args)
    {
        //пользователь задает количество элементов массива
        Console.WriteLine("Введите количество элементов массива");
        int elemArr = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Введите элементы массива");
        //пользователь задает значения элементов
        int[] mas = new int[elemArr];
        for (int i = 0; i < mas.Length; i++)
        {
            mas[i] = Convert.ToInt32(Console.ReadLine()); //преобразовавываем написанное с клавиатуры в 'int'
        }
        //Выбор класса сортировки
        Console.WriteLine("выберите тип сортировки:\n1 - Пузырьковая\n2 - Вставками");
        int type = Convert.ToInt32(Console.ReadLine());
        if (type == 1)
            mas = new Sort().bubblesort(mas);
        if (type == 2)
            mas = new Sort().insertionsort(mas);
        if (type > 2)
            Console.WriteLine("Нужно было вводить, что просили!");
        //вывод готового
        Console.WriteLine("Отсортированный массив:"); Console.WriteLine(String.Join(" ", mas));
    }
}