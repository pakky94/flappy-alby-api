export class Player {
    #area;
    #height;
    #width;

    #steps;
    #block;
    #keyboardService;

    constructor(keyboardService, area, coordinate, steps) {
        this.#keyboardService = keyboardService;
        this.#area = area;
        this.#height = area.coordinate.height;
        this.#width = area.coordinate.width;

        this.#block = area.add(coordinate, 'player');
        //document.onkeydown = e => this.#animate(e, steps);
        this.#steps = steps
    }

    get coordinate() {
        return this.#block.coordinate;
    }

    dispose() {
        this.#area.remove(this.#block);
    }

    animate() {
        this.#keyboardService.pressed.forEach(key => {
            switch (key) {
                case "ArrowUp":
                    if (this.coordinate.top > 0) {
                        this.#block.moveTop(this.#steps);
                    }
                    break;
                case "ArrowDown":
                    if (this.coordinate.top < this.#height - this.coordinate.height) {
                        this.#block.moveBottom(this.#steps);
                    }
                    break;
                case "ArrowLeft":
                    if (this.coordinate.left > 0) {
                        this.#block.moveLeft(this.#steps);
                    }
                    break;
                case "ArrowRight":
                    if (this.coordinate.left < this.#width - this.coordinate.width) {
                        this.#block.moveRight(this.#steps);
                    }
                    break;
            }
        });
    }
}
