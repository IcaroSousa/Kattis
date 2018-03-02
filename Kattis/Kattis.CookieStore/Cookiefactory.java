
import java.io.*;
import java.util.Collections;
import java.util.PriorityQueue;

class CookieFactory
{
    public static void main(String[] args)
    {
        BufferedReader _ConsoleReader = new BufferedReader(new InputStreamReader(System.in));

        PriorityQueue<Integer> _BigOnes = new PriorityQueue<>(11, Collections.reverseOrder());
        _BigOnes.add(-1);
        PriorityQueue<Integer> _SmallOnes = new PriorityQueue<>();
        _SmallOnes.add(300000001);

        String _Input;

        try
        {
            while ((_Input = _ConsoleReader.readLine()) != null)
            {
                if (_Input.contentEquals("#"))
                {
                    System.out.println(_SmallOnes.poll());
                    if (_SmallOnes.size() != _BigOnes.size())
                    {
                        _SmallOnes.add(_BigOnes.poll());
                    }
                }
                else
                {
                    int _Number = Integer.parseInt(_Input);
                    if (_Number > _SmallOnes.peek())
                    {
                        _SmallOnes.add(_Number);
                        if (_SmallOnes.size() > _BigOnes.size() +1)
                        {
                            _BigOnes.add(_SmallOnes.poll());
                        }
                    }
                    else
                    {
                        _BigOnes.add(_Number);
                        if (_BigOnes.size() > _SmallOnes.size())
                        {
                            _SmallOnes.add(_BigOnes.poll());
                        }
                    }
                }
            }
        } catch (IOException pEx)
        {

        }

    }
}
