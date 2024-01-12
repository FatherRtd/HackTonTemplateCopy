import type { ITestModel } from "@/models/TestModel";
import { httpClient } from "@/services/HttpClient/HttpClient";


export default {
    async test(): Promise<ITestModel> {
        const response = await httpClient.get<ITestModel>("api/Test/GetTest");
        return response.data;
    }
}