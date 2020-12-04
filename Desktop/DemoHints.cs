// Инициализация 
private SqlConnection name = null; 
private SqlDataAdapter name2 = null;
private DataTable name3 = null;
// Form Load
name = new SqlConnection(@"Путь подключения");
name2 = new SqlDataAdapter("Запрос SQL");
name3 = new DataTable(без параметров);
name2.Fill(name3); // заполнение бд
dataGridView1.DataSource = name3; // заполнение грида данными с бд

