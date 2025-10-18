// src/components/TaskForm.tsx

import React, { useState } from 'react';
import { useCreateTask } from '../hooks/useCreateTask';
import { createTaskSchema } from '../validation/task-schema';
import { z } from 'zod';

const TaskForm: React.FC = () => {
  const [title, setTitle] = useState('');
  const [error, setError] = useState('');

  // 1. Chamar o hook de mutação
  const { mutate, isPending, isSuccess } = useCreateTask();

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    setError(''); // Limpa erros anteriores

    try {
      const payload = createTaskSchema.parse({ title: title.trim() });
      mutate({ title });
      setTitle(''); // Limpa o campo
    } catch (err) {
      if (err instanceof z.ZodError) {
        setError(err.issues[0].message);
      }
    }
  };

  // Exemplo simples de feedback
  const buttonText = isPending ? 'Criando...' : 'Adicionar Tarefa';

  return (
    <form onSubmit={handleSubmit} style={{ marginBottom: '20px' }}>
      <input
        type="text"
        value={title}
        onChange={(e) => setTitle(e.target.value)}
        placeholder="Título da nova tarefa"
        disabled={isPending}
        style={{ padding: '8px', marginRight: '10px' }}
      />
      <button type="submit" disabled={isPending}>
        {buttonText}
      </button>

      {/* Exibe o erro de validação do frontend */}
      {error && <p style={{ color: 'red', marginTop: '5px' }}>{error}</p>}
      {isSuccess && <span style={{ marginLeft: '10px', color: 'green' }}>✓ Sucesso!</span>}
    </form>
  );
};

export default TaskForm;