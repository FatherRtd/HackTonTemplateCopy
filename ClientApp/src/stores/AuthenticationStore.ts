import { ref, computed } from 'vue';
import { defineStore } from 'pinia';
import axios from "axios";
import type User from '@/models/User';

export const useAuthenticationStore = defineStore('AuthenticationStore', () => {
  const authenticationData = ref(new AuthenticationData({} as AuthenticationResponse));

  const AuthenticationDataLiteral = "AuthenticationData";

  const login = async (login: string, password: string): Promise<AuthenticationResponse> => {    
    const request = await getAuthenticationToken(login, password);
    if(request.data.isSuccessful == false) return request.data;
    
    clearAuthenticationInStore();

    authenticationData.value = new AuthenticationData(request.data);
    setAuthenticationInStore(authenticationData.value);

    return request.data;
  }

  const register = async (user: User): Promise<AuthenticationResponse> => {    
    const request = await getRegistration(user);
    if(request.data.isSuccessful == false) return request.data;
    
    clearAuthenticationInStore();

    authenticationData.value = new AuthenticationData(request.data);
    setAuthenticationInStore(authenticationData.value);

    return request.data;
  }

  const logout = () => {    
    authenticationData.value = new AuthenticationData({} as AuthenticationResponse);
    clearAuthenticationInStore();
  }

  const refresh = async () => {
    if(authenticationData.value.refreshToken == null) {
      logout();
      return;
    }
    
    try {
      const request = await refreshAuthentication(authenticationData.value.refreshToken);
 
      clearAuthenticationInStore();
  
      authenticationData.value = new AuthenticationData(request.data);
      setAuthenticationInStore(authenticationData.value); 
    } 
    catch {
      logout();
    }
  }

  const isAuthenticated = computed((): boolean => {
    if(authenticationData.value.isEmpty()) fillAuthenticationFromStore();
    return !authenticationData.value.isEmpty();
  });

  const getAccessToken = (): string | null => {
    if(authenticationData.value.isEmpty()) fillAuthenticationFromStore();
    return authenticationData.value.accessToken;
  }

  const getAuthenticationToken = async (login: string, password: string) => {
    return await axios.post<AuthenticationResponse>( "api/token/generate", { login, password });
  }

  const getRegistration = async (user: User) => {
    return await axios.post<AuthenticationResponse>( "api/token/registration", user);
  }

  const refreshAuthentication = async (refreshToken: string) => {
    return await axios.get<AuthenticationResponse>("api/token/refresh", {   
      headers: {
        Authorization: "Bearer " + refreshToken
      }
    });
  }

  const setAuthenticationInStore = (authenticationData: AuthenticationData) => {
    window.localStorage.setItem(AuthenticationDataLiteral, JSON.stringify(authenticationData));
  }

  const fillAuthenticationFromStore = () => {
    const rawData = window.localStorage.getItem(AuthenticationDataLiteral);
    if(rawData == null) return;
    authenticationData.value = new AuthenticationData(JSON.parse(rawData) as AuthenticationResponse);
  }

  const clearAuthenticationInStore = () => {
    window.localStorage.removeItem(AuthenticationDataLiteral);
  }

  return { login, logout, register, refresh, getAccessToken, isAuthenticated }
})

class AuthenticationData {
  constructor(options: AuthenticationResponse) {
    this.expirationTime = options?.expirationTime;
    this.accessToken = options?.accessToken;
    this.refreshToken = options?.refreshToken;
  }

  expirationTime: Date | null = null;
  accessToken: string | null = null;
  refreshToken: string | null = null;

  isEmpty = () => this.expirationTime == null && this.accessToken == null && this.refreshToken == null;
}

interface AuthenticationResponse {
  expirationTime: Date | null;
  accessToken: string;
  refreshToken: string;
  isSuccessful: boolean;
  message: string;
}
