const clumsyCanvasPrefix = "ClumsyCanvas";
class ProcessInfo {
}
class Operation {
    //Из путя достаёт объект
    resolvePath(array, pInfo) {
        let path = this.readString(array, pInfo);
        let split = path.split('.');
        // @ts-ignore
        //let result: any = window[clumsyCanvasPrefix];
        let result = window;
        for (let part of split) {
            result = result[part];
        }
        return result;
    }
    readShort(array, pInfo) {
        //2 байта для определения длины строки. Грят, что этот способ не работает для отрицательных чисел, но че та ну
        //а ещё порядок байтов другой.
        let result = array[pInfo.index + 1] << 8 | array[pInfo.index];
        pInfo.index += 2;
        return result;
    }
    readString(array, pInfo) {
        let length = this.readShort(array, pInfo);
        //наверняка можно сделать итератор и не копировать строку, но мне плохо
        let readArray = new Uint8Array(length);
        for (let index = 0; index < length; index++) {
            readArray[index] = array[index + pInfo.index];
        }
        pInfo.index += length;
        return pInfo.reader.textDecoder.decode(readArray);
    }
    readByte(array, pInfo) {
        let result = array[pInfo.index];
        pInfo.index++;
        return result;
    }
}
class CreateCanvasOperation extends Operation {
    process(array, pInfo) {
        let index = this.readShort(array, pInfo);
        // @ts-ignore
        let canvas = document.createElement("canvas");
        let context = canvas.getContext("2d");
        // @ts-ignore
        window[clumsyCanvasPrefix][index] = canvas;
        canvas.ctx = context;
        pInfo.reader.canvas = canvas;
    }
}
class SetObjectOperation extends Operation {
    process(array, pInfo) {
        let index = this.readShort(array, pInfo);
        let obj = this.resolvePath(array, pInfo);
        pInfo.reader.setObject(index, obj);
    }
}
class SetElementOperation extends Operation {
    process(array, pInfo) {
        let index = this.readShort(array, pInfo);
        let elementId = this.readString(array, pInfo);
        // @ts-ignore
        let obj = document.getElementById(elementId);
        pInfo.reader.setObject(index, obj);
    }
}
class RemoveObjectOperation extends Operation {
    process(array, pInfo) {
        let index = this.readShort(array, pInfo);
        // @ts-ignore
        delete window[clumsyCanvasPrefix][index];
    }
}
class GlobalCompositeOperation extends Operation {
    process(array, pInfo) {
        let index = this.readByte(array, pInfo);
        pInfo.reader.canvas.ctx.globalCompositeOperation = GlobalCompositeOperation.dict[index];
        ;
    }
}
GlobalCompositeOperation.dict = [
    "source-over",
    "source-in",
    "source-out",
    "source-atop",
    "destination-over",
    "destination-in",
    "destination-out",
    "destination-atop",
    "lighter",
    "copy",
    "xor",
    "multiply",
    "screen",
    "overlay",
    "darken",
    "lighten",
    "color-dodge",
    "color-burn",
    "hard-light",
    "soft-light",
    "difference",
    "exclusion",
    "hue",
    "saturation",
    "color",
    "luminosity"
];
class FillRectOperation extends Operation {
    process(array, pInfo) {
        let x = this.readShort(array, pInfo);
        let y = this.readShort(array, pInfo);
        let width = this.readShort(array, pInfo);
        let height = this.readShort(array, pInfo);
        pInfo.reader.canvas.ctx.fillRect(x, y, width, height);
    }
}
class FillOperation extends Operation {
    process(array, pInfo) {
        let index = this.readByte(array, pInfo);
        pInfo.reader.canvas.ctx.fill(FillOperation.dict[index]);
    }
}
FillOperation.dict = [
    "nonzero",
    "evenodd"
];
class DrawImageOperation extends Operation {
    process(array, pInfo) {
        let option = this.readByte(array, pInfo);
        let objIndex = this.readShort(array, pInfo);
        //let obj = this.resolvePath(array, pInfo);
        let obj = pInfo.reader.getObject(objIndex);
        switch (option) {
            case 0: {
                let dx = this.readShort(array, pInfo);
                let dy = this.readShort(array, pInfo);
                pInfo.reader.canvas.ctx.drawImage(obj, dx, dy);
                return;
            }
            case 1: {
                let dx = this.readShort(array, pInfo);
                let dy = this.readShort(array, pInfo);
                let dWidth = this.readShort(array, pInfo);
                let dHeight = this.readShort(array, pInfo);
                pInfo.reader.canvas.ctx.drawImage(obj, dx, dy, dWidth, dHeight);
                return;
            }
            case 2: {
                let sx = this.readShort(array, pInfo);
                let sy = this.readShort(array, pInfo);
                let sWidth = this.readShort(array, pInfo);
                let sHeight = this.readShort(array, pInfo);
                let dx = this.readShort(array, pInfo);
                let dy = this.readShort(array, pInfo);
                let dWidth = this.readShort(array, pInfo);
                let dHeight = this.readShort(array, pInfo);
                pInfo.reader.canvas.ctx.drawImage(obj, sx, sy, sWidth, sHeight, dx, dy, dWidth, dHeight);
                return;
            }
            default:
                throw new Error("Unknown DrawImageOperation option");
        }
    }
}
class ResizeCanvasOperation extends Operation {
    process(array, pInfo) {
        let width = this.readShort(array, pInfo);
        let height = this.readShort(array, pInfo);
        pInfo.reader.canvas.width = width;
        pInfo.reader.canvas.height = height;
    }
}
class SetValueOperation extends Operation {
    process(array, pInfo) {
        let index = this.readByte(array, pInfo);
        let field = SetValueOperation.dict[index];
        let value = this.readString(array, pInfo);
        pInfo.reader.canvas.ctx[field] = value;
    }
}
SetValueOperation.dict = [
    "globalCompositeOperation",
    "fillStyle",
    "filter",
];
class SetCustomValueOperation extends Operation {
    process(array, pInfo) {
        let field = this.readString(array, pInfo);
        let value = this.readString(array, pInfo);
        pInfo.reader.canvas.ctx[field] = value;
    }
}
class SetCanvasOperation extends Operation {
    process(array, pInfo) {
        //let obj = this.resolvePath(array, pInfo);
        let objIndex = this.readShort(array, pInfo);
        let obj = pInfo.reader.getObject(objIndex);
        pInfo.reader.canvas = obj;
    }
}
class ClearRectOperation extends Operation {
    process(array, pInfo) {
        let x = this.readShort(array, pInfo);
        let y = this.readShort(array, pInfo);
        let width = this.readShort(array, pInfo);
        let height = this.readShort(array, pInfo);
        pInfo.reader.canvas.ctx.clearRect(x, y, width, height);
    }
}
class CreatePatternOperation extends Operation {
    process(array, pInfo) {
        let fromObjectIndex = this.readShort(array, pInfo);
        let patternObjIndex = this.readShort(array, pInfo);
        let patternTypeIndex = this.readByte(array, pInfo);
        let obj = pInfo.reader.getObject(fromObjectIndex);
        let pattern = pInfo.reader.canvas.ctx.createPattern(obj, CreatePatternOperation.dict[patternTypeIndex]);
        pInfo.reader.setObject(patternObjIndex, pattern);
    }
}
CreatePatternOperation.dict = [
    "repeat",
    "repeat-x",
    "repeat-y",
    "no-repeat",
];
class FillStyleOperation extends Operation {
    process(array, pInfo) {
        let option = this.readByte(array, pInfo);
        switch (option) {
            case 0: {
                //color
                let value = this.readString(array, pInfo);
                pInfo.reader.canvas.ctx.fillStyle = value;
                return;
            }
            case 1: {
                //gradient
                throw new Error("FillStyleOperation DontCare");
                return;
            }
            case 2: {
                //pattern
                let objIndex = this.readShort(array, pInfo);
                let obj = pInfo.reader.getObject(objIndex);
                pInfo.reader.canvas.ctx.fillStyle = obj;
                return;
            }
            default:
                throw new Error("FillStyleOperation Clown");
        }
    }
}
class RectOperation extends Operation {
    process(array, pInfo) {
        let x = this.readShort(array, pInfo);
        let y = this.readShort(array, pInfo);
        let width = this.readShort(array, pInfo);
        let height = this.readShort(array, pInfo);
        pInfo.reader.canvas.ctx.rect(x, y, width, height);
    }
}
class OpCodes {
}
OpCodes.dict = [
    new CreateCanvasOperation(),
    new SetObjectOperation(),
    new SetElementOperation(),
    new RemoveObjectOperation(),
    new FillOperation(),
    new GlobalCompositeOperation(),
    new FillRectOperation(),
    new ResizeCanvasOperation(),
    new DrawImageOperation(),
    new SetValueOperation(),
    new SetCustomValueOperation(),
    new SetCanvasOperation(),
    new ClearRectOperation(),
    new CreatePatternOperation(),
    new FillStyleOperation(),
    new RectOperation(),
];
class MyReader {
    constructor() {
        this.pInfo = new ProcessInfo();
        this.pInfo.reader = this;
        // @ts-ignore
        this.textDecoder = new TextDecoder("utf-8");
    }
    getObject(index) {
        // @ts-ignore
        return window[clumsyCanvasPrefix][index];
    }
    setObject(index, obj) {
        // @ts-ignore
        window[clumsyCanvasPrefix][index] = obj;
    }
    attachCanvas(canvas) {
        this.canvas = canvas;
    }
    getSingleImageData() {
        let imageData = this.canvas.ctx.getImageData(0, 0, 1, 1).data;
        return imageData[0].toString() + " " + imageData[1].toString() + " " + imageData[2].toString();
    }
    processArray(array) {
        this.pInfo.index = 0;
        while (this.pInfo.index < array.byteLength) {
            let opCode = array[this.pInfo.index];
            this.pInfo.index++;
            let op = OpCodes.dict[opCode];
            op.process(array, this.pInfo);
        }
    }
}
function initDrawReader() {
    let reader = new MyReader();
    // @ts-ignore
    window[clumsyCanvasPrefix] = {};
    // @ts-ignore
    window[clumsyCanvasPrefix].myReader = reader;
}
function processDrawArray(bytes) {
    // @ts-ignore
    let reader = window[clumsyCanvasPrefix].myReader;
    // @ts-ignore
    let byteArray = Blazor.platform.toUint8Array(bytes);
    reader.processArray(byteArray);
}
//Чтобы добавить уже существущий канвас
function attachCanvas(element, index) {
    let context = element.getContext("2d");
    element.ctx = context;
    // @ts-ignore
    let reader = window[clumsyCanvasPrefix].myReader;
    reader.setObject(index, element);
    reader.attachCanvas(element);
}
function createCanvas(index) {
    // @ts-ignore
    let canvas = document.createElement("canvas");
    attachCanvas(canvas, index);
}
function getSingleImageData() {
    // @ts-ignore
    let reader = window[clumsyCanvasPrefix].myReader;
    return reader.getSingleImageData();
}
//# sourceMappingURL=ClumsyCanvas.js.map