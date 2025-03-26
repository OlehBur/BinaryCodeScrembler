using System.Drawing;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;




const string codeS = "0100 0000 0011 1111 0011 1111 0000 0110";//0100 0000 0011 1111 0011 1111 0000 0110
const int m = 9, n = 4;
List<bool> code = new List<bool>();
List<bool> scrembler = new List<bool>();

//str to bool
foreach (char c in codeS)
    if (c != ' ')
        code.Add(c == '0' ? false : true);

//scrembler
if (code.Count == 0)
    return;

Console.WriteLine($" code = {codeS}, n = {n}, m = {m};\n");
for (int i = 0; i < code.Count; i++) {//Bi = Ai ⊕ Bi-n ⊕ Bi-m
    bool a = code[i],
        bn = GetB(scrembler, i - n),
        bm = GetB(scrembler, i - m);

    Console.WriteLine($" B{ConvertToSubscript(i)} = A{ConvertToSubscript(i)} ⊕ B{ConvertToSubscript(i - n)} ⊕ B{ConvertToSubscript(i - m)} = " +
        $"{IntFromBool(a)} ⊕ {IntFromBool(bn)} ⊕ {IntFromBool(bm)} = {Scrembler(a, bn, bm, ref scrembler)};");
}
Console.Write("\nScrembler code = ");

//result
foreach (bool b in scrembler)
    Console.Write(IntFromBool(b));
Console.WriteLine(".");

bool GetB(List<bool> B, int ind) {
    if (ind > 0)
        return B[ind];
    else
        return false;
}

int Scrembler(bool A, bool Bn, bool Bm, ref List<bool> B) {
    B.Add(A ^ Bn ^ Bm);

    return IntFromBool(B.Last());
}

int IntFromBool(bool b) {
    return (b) ?
        1 : 0;
}

static string ConvertToSubscript(int number) {
    StringBuilder subscript = new StringBuilder();

    if (number < 0) {
        subscript.Append("₋"); // Нижній індекс для мінуса
        number = -number; // Перетворюємо число в додатне для подальшого оброблення
    }

    foreach (char digit in number.ToString())
        subscript.Append(ConvertToSubscriptChar(digit));//(char)('₀' + (digit - '0')));
    return subscript.ToString();
}



static char ConvertToSubscriptChar(char digit) {
    return digit
        switch {
            '0' => '₀',
            '1' => '₁',
            '2' => '₂',
            '3' => '₃',
            '4' => '₄',
            '5' => '₅',
            '6' => '₆',
            '7' => '₇',
            '8' => '₈',
            '9' => '₉',
            _ => digit
        };
}



public struct Tochka {
    public float X { get; set; }
    public float Y { get; set; }

    public Tochka(float x, float y) {
        X = x;
        Y = y;
    }
}

public enum Types { Quadrangle, Rectangle, Square, Rhomb, Paralelogram, Trapeze };

public class TQuadrangle {

    public List<Tochka> points { get; set; } = new List<Tochka>(4);
    public Types type { get; private set; } = Types.Quadrangle;


    public TQuadrangle(Tochka[] points) {
        foreach (Tochka t in points)
            this.points.Add(t);

        TypeDef();
    }

    void TypeDef() {
        if (IsRect()) {
            if (IsSquare()) {
                type = Types.Square;
                return;
            }
            type = Types.Rectangle;

            return;
        }

        if (IsRhomb()) {
            type = Types.Rhomb;
            return;
        }

        if (IsParalelogram()) {
            type = Types.Paralelogram;
            return;
        }
        if (IsTrapeze()) {
            type = Types.Trapeze;
            return;
        }

        type = Types.Quadrangle;
    }

    public float Perimeter {
        get {
            float perimeter = 0f;
            for (int i = 0, nextI = 0; i < points.Count; i++) {
                nextI = (i + 1 >= points.Count) ? 
                    0 : i + 1;

                perimeter += GetVidrizokLength(points[i], points[nextI]);
            }

            return perimeter;
        }
    }

    private float GetVidrizokLength(Tochka a, Tochka b) {
        return MathF.Sqrt(
            MathF.Pow(b.X - a.X, 2)
            + MathF.Pow(b.Y - a.Y, 2));
    }

    private bool IsRect() {
        return (GetVidrizokLength(A, B) == GetVidrizokLength(C, D) && GetVidrizokLength(B, C) == GetVidrizokLength(D, A));
    }

    private bool IsSquare() {
        return (IsRect() && GetVidrizokLength(A, B) == GetVidrizokLength(B, C));
    }

    private bool IsRhomb() {
        return (GetVidrizokLength(A, B) == GetVidrizokLength(B, C) && GetVidrizokLength(B, C) == GetVidrizokLength(C, D) && GetVidrizokLength(C, D) == GetVidrizokLength(D, A));
    }

    private bool IsParalelogram() {
        return (GetVidrizokLength(A, B) == GetVidrizokLength(C, D) && GetVidrizokLength(B, C) == GetVidrizokLength(D, A));
    }

    private bool IsTrapeze() {
        return (GetVidrizokLength(A, B) == GetVidrizokLength(C, D) || GetVidrizokLength(B, C) == GetVidrizokLength(D, A));
    }

    public override string ToString() {
        return $"TQuadrangle has {points.ToString()}) points, have a {type} type, and perimeter: {Perimeter} un.";
    }
}
