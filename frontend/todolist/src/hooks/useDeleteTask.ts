import { useMutation,useQueryClient } from "@tanstack/react-query";
import apiClient from "../api/config";

const deleteTask = async (Id: number) => {
  const response = await apiClient.delete(`/Tasks/${Id}`);
  return response.data;
};

export const useDeleteTask = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deleteTask,
    onSuccess: () => {
      // Invalida e refaz a consulta das tarefas após a exclusão
      queryClient.invalidateQueries({ queryKey: ['tasks'] });
    },

    onError: (error) => {
      console.error("Erro ao excluir a tarefa:", error);
    },
  });
};