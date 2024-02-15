using System;
using System.Collections;
using System.Collections.Generic;

public class Primo : IEnumerable<int>
{
    int numElementos;

    public Primo(int numer) { this.numElementos = numer; }

    public IEnumerator<int> GetEnumerator()
    {
        return new PrimoYoTeEnumero(numElementos);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public class PrimoYoTeEnumero : IEnumerator<int>
{
    private int numElementos;
    private int current;
    private int count;

    public PrimoYoTeEnumero(int numeros)
    {
        numElementos = numeros;
        current = 1;
        count = 0;
    }

    public int Current => current;

    object IEnumerator.Current => Current;

    public void Dispose()
    {
        // No hay recursos para liberar en este caso
    }

    public bool MoveNext()
    {
        while (count < numElementos)
        {
            current++;
            if (EsPrimo(current))
            {
                count++;
                return true;
            }
        }
        return false;
    }

    public void Reset()
    {
        current = 1;
        count = 0;
    }

    private bool EsPrimo(int numero)
    {
        if (numero <= 1)
            return false;

        if (numero <= 3)
            return true;

        if (numero % 2 == 0 || numero % 3 == 0)
            return false;

        int i = 5;
        while (i * i <= numero)
        {
            if (numero % i == 0 || numero % (i + 2) == 0)
                return false;
            i += 6;
        }

        return true;
    }
}


