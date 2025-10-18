import { z } from 'zod';

// Schema para a criação de tarefa
export const createTaskSchema = z.object({
    title: z
      .string()
      .min(1, { message: 'O título é obrigatório.' })
      .min(1, 'O título não pode estar vazio.')
      .max(100, 'O título deve ter no máximo 100 caracteres.'), // Limite igual ao backend
  });

  export type CreateTaskPayload = z.infer<typeof createTaskSchema>;