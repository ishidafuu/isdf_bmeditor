<template>
  <div>
    <button @click="undo">Undo (Ctrl+Z)</button>
    <button @click="redo">Redo (Ctrl+Shift+Z)</button>
  </div>
</template>

<script lang="ts">
import { defineComponent, onBeforeUnmount } from 'vue'
import { useStore } from 'vuex'

export default defineComponent({
  props: {
    storeName: {
      type: String,
      required: true
    },
  },
  setup(props) {
    const store = useStore()

    const undo = () => {
      store.commit(`${props.storeName}/undo`)
    }

    const redo = () => {
      store.commit(`${props.storeName}/redo`)
    }

    const handleKeydown = (event: KeyboardEvent) => {
      if (event.ctrlKey) {
        if (event.key.toLowerCase() === 'z') {
          if (event.shiftKey) {
            redo()
          } else {
            undo()
          }
          event.preventDefault()
        }
      }
    }

    window.addEventListener('keydown', handleKeydown)

    onBeforeUnmount(() => {
      window.removeEventListener('keydown', handleKeydown.value)
    })

    return {
      undo,
      redo
    }
  },
})
</script>
