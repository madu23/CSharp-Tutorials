export class SubMenu {
    public title: string;
    public items: string[];

    constructor(title: string, items: string[]) {
        this.title = title;
        this.items = items;
    }

    public displaySubMenu(): void {
        console.log(this.title);
        this.items.forEach((item, index) => {
            console.log(`${index + 1}: ${item}`);
        });
    }

    public handleItemSelection(selection: number): void {
        if (selection > 0 && selection <= this.items.length) {
            console.log(`You selected: ${this.items[selection - 1]}`);
        } else {
            console.log('Invalid selection. Please try again.');
        }
    }
}