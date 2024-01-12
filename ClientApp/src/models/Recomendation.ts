export interface IRecomendation {
    value: number | null | undefined,
    title: string
}

export default class Recomendation {
    constructor(options: IRecomendation | null) {
        if(options == null)
            return;

        this.value = options.value;
        this.title = options.title;
    }

    value: number | null | undefined;
    title = "";
}