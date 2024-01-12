import axios from "axios";
import { useAuthenticationStore } from "@/stores/AuthenticationStore";

export const httpClient = axios.create();

httpClient.interceptors.request.use(
    async (config) => {
        const authenticationStore = useAuthenticationStore();
        const accessToken = authenticationStore.getAccessToken();

        config.headers.Authorization = `Bearer ${ accessToken }`

        return config
    }
  );

httpClient.interceptors.response.use(
  (config) => {
    return config;
  },
  async (error) => {
   const originalRequest = {...error.config};
   originalRequest._isRetry = true; 
    if (
      error.response.status === 401 && 
      error.config &&
      !error.config._isRetry
    ) {
        const authenticationStore = useAuthenticationStore();
        await authenticationStore.refresh();

        return httpClient.request(originalRequest);
    }
    throw error;
  }
);