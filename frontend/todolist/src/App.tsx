import { useTasks } from './hooks/useTasks'

function App() {
  // Chamada do nosso custom hook que gerencia o estado da requisição
  const { data: tasks, isLoading, isError, error } = useTasks();

  if (isLoading) {
    return <div>Carregando tarefas...</div>;
  }

  if (isError) {
    // Exibe o erro retornado pelo Axios ou pela API
    console.error(error);
    return <div>Ocorreu um erro ao carregar as tarefas: {(error as Error).message}</div>;
  }

  return (
    <div style={{ padding: '20px' }}>
      <h1>Lista de Tarefas</h1>

      {tasks && tasks.length === 0 && (
        <p>Nenhuma tarefa encontrada. Que tal criar uma?</p>
      )}

      <ul>
        {tasks?.map(task => (
          <li
            key={task.id}
            style={{ textDecoration: task.isCompleted ? 'line-through' : 'none' }}
          >
            {task.title}
            {task.isCompleted && ' (Concluída)'}
          </li>
        ))}
      </ul>
    </div>
  )
}

export default App
