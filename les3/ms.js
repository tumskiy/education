function ms (array)
{
  if (array.length > 1)
  {
    let mid = Math.floor(array.length/2)
    let lh = array.slice(0, mid);
    let rh = array.slice(mid);

    ms(lh);
    ms(rh);

    let l = 0;
    let r = 0;
    let k = 0;
    while (l < lh.length && r < rh.length)
    {
      if (lh[l] < rh [r])
        {
          array[k] = lh[l];
          l++;
        }
      else
        {
          array[k] = rh[l];
          r++;
        }
        k++;
    }        
    while (l < lh.length)
    {
      array[k] = lh[l];
      l++;
      k++;
    }
    while (r < rh.length)
    {
      array[k] = rh[r];
      r++;
      k++;
    }  
  }
  return array
};
let a = [45,12,77,8,46,78,23,1,8988,165,65536];
console.log(ms(a));
