//4.Напишите функцию, которая в качестве аргумента принимает натуральное число n и возвращает сумму цифр этого числа. 
//Если это значение имеет более одной цифры, продолжайте уменьшать его таким образом, пока не будет получено одноразрядное число.
//Это применимо только к натуральным числам. 
int funcSum(int a)
{
    int result = 0;
    string array = Convert.ToString(a);//бахаем в строку
    int[] numbers = array.Select(x => int.Parse(x.ToString())).ToArray();//строку в массив инт
    for (int i = 0; i < numbers.Length; i++)//цикл for для индексации массива
    {
        result +=  numbers[i];//сложение до победного
    }
    if (result.ToString().Length > 1)//если длина массива больше 1
        return funcSum(result);//рекурсия
    else
        return result;
}
int a = 65536;
Console.WriteLine($"Сумма чисел числа '{a}':"+funcSum(a));

//6.Напишите функцию, которая может принимать любое неотрицательное целое число
//в качестве аргумента и возвращать его вместе с цифрами в порядке убывания. 
//Переставьте цифры так, чтобы на выходе создать максимально возможное число.
int[] BigNumber(int a)
{
    string array = Convert.ToString(a);//бахаем в строку
    int[] numbers = array.Select(x => int.Parse(x.ToString())).ToArray();//строку в массив инт
    for (int i = 0; i < numbers.Length; i++)
    {
        for (int j = 0; j < numbers.Length - 1; j++)
        {
            if (numbers[j] < numbers[j + 1]) //если значение текущего индекса больше следующего
            {
                int temp = numbers[j + 1]; //временная переменная
                numbers[j + 1] = numbers[j]; //смена мест
                numbers[j] = temp; //смены следующего на текущий индекс
            }
        }
    }
    return numbers;
}
int[] sort = BigNumber(12345);
Console.WriteLine("Сортировка в большое число:"+(String.Join("",sort)));

//7.Дана пирамида чисел:
//              1
//          3     5
//       7     9    11
//   13    15    17    19
//21    23    25    27    29...
//Напишите функцию, которая вычисляет сумму строки этого треугольника из переданного в функцию индекса строки (начиная с индекса 1).

int[,] array = new int[5, 5]
{
    {0,0,1,0,0},
    {0,0,3,5,0},
    {0,7,9,11,0},
    {0,13,15,17,19},
    {21,23,25,27,29}
};

int SumArray(int[,] array, int row)
{    
    for (int col = 1; col < array.GetLength(0); col++)
    {
        Console.WriteLine($"Строка:{row}" +
            $" Столбец:{col}");
        int[] ar = (int[])array.GetValue(row);
        int sum = 0;
        Console.WriteLine(ar);
    }
    Console.WriteLine("///////////////");    
    return 0;
}

Console.WriteLine(SumArray(array, 1)); 




//8.Напишите функцию, которая не принимает аргументов и всегда возвращает 5. 
//Звучит просто, не правда ли? Просто имейте в виду, что вы не можете использовать ни один из следующих символов: 
//0 1 2 3 4 5 6 7 8 9 * +- /
int Return5()
{
    string arr = "qwert";
    int five = arr.Length;
    Console.WriteLine("Возвращаем пять:"+five);
    return five;
}
Return5();