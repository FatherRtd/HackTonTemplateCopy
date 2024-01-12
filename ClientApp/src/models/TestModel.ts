export interface ITestModel {
    value: string
}

export default class TestModel {
    constructor(options: ITestModel) {
        this.value = options.value
    }

    value = ""
}