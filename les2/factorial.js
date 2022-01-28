function fact (f)
{
    if (f != 0)
    {
        if (f === 1)
            return 1;   
        else
            return f * fact(f-1);
    }
    else
    console.log("не вводи ноль, дурачок");
}
let res = fact(2); 
console.log(res);