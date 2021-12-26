let array = [45,78,936,11];
function insertionSort(arr)
{
  for (let i = 1; i<arr.length; i++) // цикл на длину массива
  {    
    let num = arr[i]; 
    let j =i;      
    if (j>0 && arr[j-1]>num)
    {
      arr[j] = arr[j-1];
      j--;
    }
    arr[j] = num;
  }
};
console.log(insertionSort(array.join(" ")));