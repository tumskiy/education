function MergeSort (array)
{
  if (array.length > 1)
  {
    let mid = Math.floor(array.length/2)
    let LeftHalf = array.slice(0, mid);
    let RightHalf = array.slice(mid);

    MergeSort(LeftHalf);
    MergeSort(RightHalf);

    let leftIndex = 0;
    let rightIndex = 0;
    let index = 0;
    while (leftIndex < LeftHalf.length && rightIndex < RightHalf.length)
    {
      if (LeftHalf[leftIndex] < RightHalf[rightIndex])
        {
          array[index] = LeftHalf[leftIndex];
          leftIndex++;
        }
      else
        {
          array[index] = RightHalf[leftIndex];
          rightIndex++;
        }
        index++;
    }        
    for(leftIndex < LeftHalf.length; (array[index] = LeftHalf[leftIndex]); leftIndex++)
    {
      index++;
    }
    for(rightIndex < RightHalf.length; (array[index] = RightHalf[rightIndex]); rightIndex++)
    {
      index++;      
    }  
  }
  return array
};
let a = [45,12,77,8,46,78,23,1,8988,165,65536];
console.log(MergeSort(a));
