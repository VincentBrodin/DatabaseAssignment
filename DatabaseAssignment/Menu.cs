namespace DatabaseAssignment;

internal class Menu {
	public int PromptChoise(IEnumerable<string> options, IEnumerable<string>? shorthands = default) {
		var choice = -1;
		do {
			const string SPACER = "    ";
			Console.WriteLine("Choose option:");
			for(var i = 0; i < options.Count(); i++) {
				string option = options.ElementAt(i);
				if(shorthands != null && shorthands.Count() > i) {
					string shorthand = shorthands.ElementAt(i);
					Console.WriteLine($"[{i + 1}]{SPACER}{option} ({shorthand})");
				}
				else {
					Console.WriteLine($"[{i + 1}]{SPACER}{option}");
				}
			}

			var input = GetString("Enter choice: ").ToLower();
			if(int.TryParse(input, out choice)) {
				choice -= 1;
			}
			else {
				for(var i = 0; i < options.Count(); i++) {
					string option = options.ElementAt(i).ToLower();
					string shorthand = option;
					if(shorthands != null && shorthands.Count() > i) {
						shorthand = shorthands.ElementAt(i);
					}

					if(option == input || shorthand == input) {
						choice = i;
					}
				}
			}
		}
		while(choice == -1);
		return choice;
	}

	public static string GetString(string prompt) {
		string? input = null;
		do {
			Console.Write(prompt);
			input = Console.ReadLine();
		}
		while(string.IsNullOrEmpty(input));
		return input;
	}
}
