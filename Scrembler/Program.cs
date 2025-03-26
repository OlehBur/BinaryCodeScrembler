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