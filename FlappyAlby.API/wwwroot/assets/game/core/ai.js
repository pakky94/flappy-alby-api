﻿export class Ai {
    #area;
    #schema;
    #onStepOver;
    #lastTimestamp;
    #player

    #blocks = [];

    constructor(area, schema, onStepOver) {
        this.#area = area;
        this.#schema = schema;
        this.#onStepOver = onStepOver;
    }

    start(player, steps) {
        this.#player = player;
        let playerCoordinate = player.coordinate;
        this.#build(playerCoordinate);

        window.requestAnimationFrame(timestamp => this.#animate(timestamp, playerCoordinate, steps));
    }

    stop() {
        for (let blocks of this.#blocks) {
            this.#area.remove(blocks);
        }

        this.#blocks = [];
    }

    crash(playerCoordinate) {
        return this.#blocks.some(blocks => blocks.some(block => block.crash(playerCoordinate)));
    }

    #animate = (timestamp, playerCoordinate, steps = 1) => {
        this.#lastTimestamp = this.#lastTimestamp ?? timestamp;

        let timestep = timestamp - this.#lastTimestamp;
        this.#lastTimestamp = timestamp;

        this.#player.animate();

        if (this.#blocks.length <= 0) return;

        // move all blocks left
        for (let blocks of this.#blocks) {
            for (let block of blocks) {
                block.moveLeft(steps);
            }
        }

        // destroy blocks if they are out of screen
        const destroy = this.#blocks[0].some(block => block.coordinate.left + block.coordinate.width <= 0);
        if (destroy) {
            const blocks = this.#blocks.shift();
            this.#area.remove(blocks);
        }

        // create new blocks if the previous block is at 2/3 of screen
        const limit = Math.floor(this.#area.coordinate.width * .66);
        const left = this.#blocks[this.#blocks.length - 1][0].coordinate.left; // TODO: calc max left coordinate in sub-array
        if (left <= limit || limit >= left) {
            this.#build(playerCoordinate);
        }

        // callback to game class
        const $continue = this.#onStepOver(timestep);

        if ($continue) {
            window.requestAnimationFrame(newTimestamp => this.#animate(newTimestamp, playerCoordinate, steps));
        }
    }

    #build(playerCoordinate) {
        const coordinates = this.#schema(this.#area.coordinate, playerCoordinate);

        const blocks = [];
        for (let coordinate of coordinates) {
            const block = this.#area.add(coordinate);
            blocks.push(block);
        }

        this.#blocks.push(blocks);
    }
}
