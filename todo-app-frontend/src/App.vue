<template>
  <div class="max-w-md mx-auto p-4">
    <h1 class="text-2xl font-bold mb-4">To Do List</h1>
    <input
      v-model="newTodo"
      @keyup.enter="addTodo"
      placeholder="Adicionar nova tarefa"
      class="border p-2 w-full"
    />
    <ul class="mt-4">
      <li
        v-for="todo in todos"
        :key="todo.id"
        class="flex justify-between items-center border-b py-2"
      >
        <span :class="{ 'line-through': todo.isCompleted }">
          {{ todo.title }}
        </span>
        <button
          @click="toggleComplete(todo)"
          class="bg-blue-500 text-white px-2 py-1 rounded"
        >
          Toggle
        </button>
      </li>
    </ul>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  data() {
    return {
      todos: [],
      newTodo: ''
    }
  },
  methods: {
    fetchTodos() {
      axios.get('http://localhost:5007/todo').then(response => {
        this.todos = response.data
      })
    },
    addTodo() {
      if (!this.newTodo) return
      const todo = { id: Date.now(), title: this.newTodo, isCompleted: false }
      axios.post('http://localhost:5007/todo', todo).then(() => {
        this.todos.push(todo)
        this.newTodo = ''
      })
    },
    toggleComplete(todo) {
      todo.isCompleted = !todo.isCompleted
      axios.put(`http://localhost:5007/todo/${todo.id}`, todo).then(() => {
        this.fetchTodos()
      })
    }
  },
  mounted() {
    this.fetchTodos()
  }
}
</script>
