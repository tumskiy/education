function MergeSort (array)//объявляем функцию
{
  if (array.length > 1) //если длина массива больше 1
  {
    let mid = Math.floor(array.length/2) //середина
    let LeftHalf = MergeSort(array.slice(0, mid)); // левая часть
    let RightHalf = MergeSort(array.slice(mid)); // правая часть

    let leftIndex = 0; // все три индекса по 0
    let rightIndex = 0;
    let index = 0;
    while (leftIndex < LeftHalf.length && rightIndex < RightHalf.length) //пока левый индекс меньше длины левой половины и правый индекс меньше длины правой половины
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
    while(leftIndex < LeftHalf.length) //пока индекс левого масиива меньше длины этого масиива
    {
      array[index] = LeftHalf[leftIndex]; //индекс изначального масиива равен индексу левого масиива
      leftIndex++;    
      index++;
    }
    while(rightIndex < RightHalf.length) // и наоборот
    {
      array[index] = RightHalf[rightIndex]; 
      rightIndex++;    
      index++;      
    }  
  }
  return array
};
let a = [45,12,77,8,46,78,23,1,8988,165,65536];
console.log(MergeSort(a));
