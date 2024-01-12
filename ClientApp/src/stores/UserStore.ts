import { ref } from 'vue';
import { defineStore } from 'pinia';

import User, { type IUser } from "@/models/User";
import UserService from "@/services/UserService";

export const useUserStore = defineStore('UserStore', () => {
  const userData = ref(new User({} as IUser));

  const setCurrentUser = (user: User) => {
    userData.value = user;
  }

  const getCurentUser = async (): Promise<User> => {
    if(userData.value.isEmpty()) await fillCurrentUser();
    return userData.value;
  }

  const fillCurrentUser = async () => {
    const userResponse = await UserService.getCurentUser(); 
    setCurrentUser(new User(userResponse));
  }

  const clearCurrentUser = () => {
    userData.value = new User({} as IUser);
  }

  return { getCurentUser, fillCurrentUser, clearCurrentUser }
})
