let array = [45,78,936,11,54,8,7984,15,996,32];
function insertionSort(arr)
{
  for (let i = 1; i<arr.length; i++) // цикл на длину массива
  {    
    let num = arr[i]; 
    let j = i-1;
    while ((j+1>0) && arr[j]>num) 
    {
      arr[j+1] = arr[j];
      j--;
    }  
    arr[j+1] = num  
  }
  return arr;
};
console.log(insertionSort(array));