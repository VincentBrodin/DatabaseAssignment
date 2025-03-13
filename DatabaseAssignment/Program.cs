namespace DatabaseAssignment;

internal static class Program {
	private static void Main(string[] args) {
		var dbCtx = new AppDbContext();

		var options = new string[] { "Show all students", "Add student", "Edit student", "Remove student", "Exit" };
		var shorthands = new string[] { "all", "add", "edit", "del" };
		var menu = new Menu();

		bool running = true;
		do {
			int choice = menu.PromptChoise(options, shorthands);
			switch(choice) {
				case 0:
					ListStudents(dbCtx);
					break;
				case 1:
					AddStudent(dbCtx);
					break;
				case 2:
					EditStudent(dbCtx);
					break;
				case 3:
					RemoveStudent(dbCtx);
					break;
				default:
					running = false;
					break;
			}
		} while(running);

		dbCtx.Dispose();
	}

	private static void ListStudents(AppDbContext dbContext) {
		foreach(var student in dbContext.Students.ToList()) {
			Console.WriteLine($"[{student.StudentId}] {student.FirstName} {student.LastName} @ {student.City}");
		}
	}

	private static void AddStudent(AppDbContext dbCtx) {
		Console.WriteLine("Add student");
		string firstName = Menu.GetString("First name: ");
		string lastName = Menu.GetString("Last name: ");
		string city = Menu.GetString("City: ");
		var student = new Student() {
			FirstName = firstName,
			LastName = lastName,
			City = city,
		};
		dbCtx.Add(student);
		dbCtx.SaveChanges();
	}

	private static void EditStudent(AppDbContext dbCtx) {
		Console.WriteLine("Which student do you want to edit");
		Console.WriteLine("Leave blank if no change");
		var menu = new Menu();
		var students = dbCtx.Students.OrderBy(s => s.StudentId);
		int choice = menu.PromptChoise(students.Select(s => $"{s.FirstName} {s.LastName} {s.City}"));
		if(choice >= students.Count()) {
			Console.WriteLine($"{choice + 1} is out of bounds");
			return;
		}
		var student = students.ElementAt(choice);

		//First name
		Console.Write($"First name ({student.FirstName}): ");
		string? firstName = Console.ReadLine();
		if(!string.IsNullOrEmpty(firstName)) {
			student.FirstName = firstName;
		}

		//Last name
		Console.Write($"Last name ({student.LastName}): ");
		string? lastName = Console.ReadLine();
		if(!string.IsNullOrEmpty(lastName)) {
			student.LastName = lastName;
		}

		//City
		Console.Write($"City ({student.City}): ");
		string? city = Console.ReadLine();
		if(!string.IsNullOrEmpty(city)) {
			student.City = city;
		}
		dbCtx.SaveChanges();
	}

	private static void RemoveStudent(AppDbContext dbCtx) {
		Console.WriteLine("Which student do you want to remove");
		var menu = new Menu();
		var students = dbCtx.Students.OrderBy(s => s.StudentId);
		int choice = menu.PromptChoise(students.Select(s => $"{s.FirstName} {s.LastName} {s.City}"));
		var student = students.ElementAt(choice);
		dbCtx.Remove(student);
		dbCtx.SaveChanges();
	}
}
