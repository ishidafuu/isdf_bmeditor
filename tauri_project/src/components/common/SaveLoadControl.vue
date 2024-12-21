<template>
  <div>
    <button @click="saveState">Save State (Ctrl+S)</button>
    <button @click="loadState">Load State</button>
    <button @click="openSaveDirectory">Open Save Directory</button>
    <div v-if="fileStatus === 'SaveComplete'">Save Complete</div>
    <div v-if="fileStatus === 'LoadComplete'">Load Complete</div>
  </div>
</template>

<script lang="ts">
import {defineComponent, onBeforeUnmount, ref} from 'vue'
import {useStore} from 'vuex'
import {invoke} from '@tauri-apps/api/tauri'

export default defineComponent({
  props: {
    storeName: {
      type: String,
      required: true
    },
  },
  setup(props) {
    const store = useStore()
    const fileStatus = ref('')

    const saveState = async () => {
      await store.dispatch(`${props.storeName}/saveState`)
      fileStatus.value = 'SaveComplete'
      setTimeout(() => fileStatus.value = '', 1000)
    }

    const loadState = () => {
      store.dispatch(`${props.storeName}/loadState`)
      fileStatus.value = 'LoadComplete'
      setTimeout(() => fileStatus.value = '', 1000)
    }

    const openSaveDirectory = async () => {
      const savePath = await store.dispatch(`${props.storeName}/getSaveDir`)
      await invoke('open_save_directory', {path: savePath})
    }

    const handleKeydown = (event: KeyboardEvent) => {
      if (event.ctrlKey && event.key === 's') {
        saveState()
      }
    }

    window.addEventListener('keydown', handleKeydown)

    onBeforeUnmount(() => {
      window.removeEventListener('keydown', handleKeydown)
    })

    return {
      saveState,
      loadState,
      fileStatus,
      openSaveDirectory,
    }
  },
})
</script>
