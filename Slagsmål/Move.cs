class Move
{
    public string name { get; set; }
    public int accuracy { get; set; }//0-100
    public int damage { get; set; }

    public Move(string name, int accuracy, int damage)
    {
        this.name = name;
        this.accuracy = accuracy;
        this.damage = damage;
    }
}