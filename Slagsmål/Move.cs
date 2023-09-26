class Move
{
    public string Name { get; set; }
    public int Accuracy { get; set; }//0-100
    public int Damage { get; set; }

    public Move(string name, int accuracy, int damage)
    {
        Name = name;
        Accuracy = accuracy;
        Damage = damage;
    }
}