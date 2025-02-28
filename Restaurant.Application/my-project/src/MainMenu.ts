class MainMenu {
    public title: string;
    public options: string[];

    constructor(title: string, options: string[]) {
        this.title = title;
        this.options = options;
    }

    public displayMenu(): void {
        console.log(this.title);
        this.options.forEach((option, index) => {
            console.log(`${index + 1}: ${option}`);
        });
    }

    public handleSelection(selection: number): void {
        if (selection > 0 && selection <= this.options.length) {
            console.log(`You selected: ${this.options[selection - 1]}`);
        } else {
            console.log('Invalid selection. Please try again.');
        }
    }
}