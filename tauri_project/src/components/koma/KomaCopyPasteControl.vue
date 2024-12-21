<template>
  <div>
    <button @click="copyKoma()">CopyKoma (Ctrl+C)</button>
    <button @click="pasteKoma()">PasteKoma (Ctrl+V)</button>
  </div>
</template>

<script lang="ts">
import { defineComponent, onMounted, onBeforeUnmount, toRefs } from 'vue'
import { useStore } from 'vuex'

export default defineComponent({
  props: {
    storeName: {
      type: String,
      required: true
    }
  },
  setup(props) {
    const store = useStore()
    const { storeName } = toRefs(props)

    const copyKoma = () => {
      store.commit(`${storeName.value}/copyKoma`)
    }

    const pasteKoma = () => {
      store.commit(`${storeName.value}/pasteKoma`)
    }

    const handleKeyDown = (event: KeyboardEvent) => {
      if (event.ctrlKey && event.key === 'c') {
        copyKoma()
      }

      if (event.ctrlKey && event.key === 'v') {
        pasteKoma()
      }
    }

    onMounted(() => {
      window.addEventListener('keydown', handleKeyDown)
    })

    onBeforeUnmount(() => {
      window.removeEventListener('keydown', handleKeyDown)
    })

    return {
      copyKoma,
      pasteKoma,
    }
  }
})
</script>
